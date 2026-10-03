using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceCreate
{
    internal class CatalogPriceCreateCommandHandler : IRequestHandler<CatalogPriceCreateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogPriceCreateRepository CatalogPriceCreateRepository;
        private readonly ICatalogPriceVerifyFieldsRepository CatalogPriceVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogPriceCreateCommandHandler(
            ICatalogPriceCreateRepository CatalogPriceCreateRepository,
            ICatalogPriceVerifyFieldsRepository CatalogPriceVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogPriceCreateRepository = CatalogPriceCreateRepository;
            this.CatalogPriceVerifyFieldsRepository = CatalogPriceVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogPriceCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogPrice.Create(
                            CurrentSessionService.CompanyID,
                            Request.CatalogPresentationID,
                            Request.EstablishmentID,
                            Request.PriceTypeID,
                            Request.CurrencyTypeID,
                            Request.CatalogPriceAmount,
                            Request.CatalogPriceIsTaxIncluded,
                            Request.RecordOriginID,
                            Request.RecordStateID,
                            DateTime.Now,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );   

                  await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogPriceVerifyFieldsRepository.VerifyFieldsAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogPrice.OK)
                {
                    int RecordAffected = await CatalogPriceCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {                         
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogPriceID                        
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
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGPRICE_FIELDS);                     
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
