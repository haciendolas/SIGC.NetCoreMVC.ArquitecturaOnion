using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxCreate
{
    internal class CatalogTaxCreateCommandHandler : IRequestHandler<CatalogTaxCreateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogTaxCreateRepository CatalogTaxCreateRepository;
        private readonly ICatalogTaxVerifyFieldsRepository CatalogTaxVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogTaxCreateCommandHandler(
            ICatalogTaxCreateRepository CatalogTaxCreateRepository,
            ICatalogTaxVerifyFieldsRepository CatalogTaxVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogTaxCreateRepository = CatalogTaxCreateRepository;
            this.CatalogTaxVerifyFieldsRepository = CatalogTaxVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogTaxCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogTax.Create(
                            CurrentSessionService.CompanyID,
                            Request.CatalogID,
                            Request.TaxID,
                            Request.TaxAffectationTypeID,
                            Request.RecordOriginID,
                            Request.RecordStateID,
                            DateTime.Now,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );   

                  await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogTaxVerifyFieldsRepository.VerifyFieldsAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogTax.OK)
                {
                    int RecordAffected = await CatalogTaxCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {                         
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogTaxID                        
                        }; 
                    }
                    else {
                        await UnitOfWork.RollbackTransactionAsync(CancellationToken); 
                      
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else{
                    await UnitOfWork.RollbackTransactionAsync(CancellationToken); 
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGTAX_FIELDS);                     
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = ae.Message;
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync(CancellationToken); 
         
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }
    }
}
