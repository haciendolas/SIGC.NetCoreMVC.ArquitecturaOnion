using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceUpdate
{
    internal class CatalogPriceUpdateCommandHandler : IRequestHandler<CatalogPriceUpdateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogPriceUpdateRepository CatalogPriceUpdateRepository;
        private readonly ICatalogPriceVerifyFieldsRepository CatalogPriceVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogPriceUpdateCommandHandler(
            ICatalogPriceUpdateRepository CatalogPriceUpdateRepository,
            ICatalogPriceVerifyFieldsRepository CatalogPriceVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogPriceUpdateRepository = CatalogPriceUpdateRepository;
            this.CatalogPriceVerifyFieldsRepository = CatalogPriceVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogPriceUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogPrice.Update(
                            CurrentSessionService.CompanyID,
                            Request.CatalogPriceID,
                            Request.CatalogPresentationID,
                            Request.EstablishmentID,
                            Request.PriceTypeID,
                            Request.CurrencyTypeID,
                            Request.CatalogPriceAmount,
                            Request.CatalogPriceIsTaxIncluded,                           
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
                    int RecordAffected = await CatalogPriceUpdateRepository.UpdateAsync(Model, CancellationToken);
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
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_UPDATE);
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
