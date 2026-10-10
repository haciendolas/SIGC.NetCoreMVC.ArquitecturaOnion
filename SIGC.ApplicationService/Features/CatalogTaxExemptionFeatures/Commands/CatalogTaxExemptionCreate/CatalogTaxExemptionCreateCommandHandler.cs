using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionCreate
{
    internal class CatalogTaxExemptionCreateCommandHandler : IRequestHandler<CatalogTaxExemptionCreateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogTaxExemptionCreateRepository CatalogTaxExemptionCreateRepository;
        private readonly ICatalogTaxExemptionVerifyFieldsRepository CatalogTaxExemptionVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogTaxExemptionCreateCommandHandler(
            ICatalogTaxExemptionCreateRepository CatalogTaxExemptionCreateRepository,
            ICatalogTaxExemptionVerifyFieldsRepository CatalogTaxExemptionVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogTaxExemptionCreateRepository = CatalogTaxExemptionCreateRepository;
            this.CatalogTaxExemptionVerifyFieldsRepository = CatalogTaxExemptionVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogTaxExemptionCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogTaxExemption.Create(
                            CurrentSessionService.CompanyID,                         
                            Request.EstablishmentID,
                            Request.CatalogTaxID,                           
                            Request.RecordOriginID,
                            Request.RecordStateID,
                            DateTime.Now,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );   

                  await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogTaxExemptionVerifyFieldsRepository.VerifyFieldsAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogTaxExemption.OK)
                {
                    int RecordAffected = await CatalogTaxExemptionCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {                         
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogTaxExemptionID                        
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
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGTAXEXEMPTION_FIELDS);                     
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
