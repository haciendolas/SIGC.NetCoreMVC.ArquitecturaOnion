using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogStockRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogStockFeatures.Commands.CatalogStockCreate
{
    internal class CatalogStockCreateCommandHandler : IRequestHandler<CatalogStockCreateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogStockCreateRepository CatalogStockCreateRepository;
        private readonly ICatalogStockVerifyFieldsRepository CatalogStockVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogStockCreateCommandHandler(
            ICatalogStockCreateRepository CatalogStockCreateRepository,
            ICatalogStockVerifyFieldsRepository CatalogStockVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogStockCreateRepository = CatalogStockCreateRepository;
            this.CatalogStockVerifyFieldsRepository = CatalogStockVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogStockCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogStock.Create(
                            CurrentSessionService.CompanyID,
                            Request.CatalogVariantID,
                            Request.WarehouseID,
                            Request.CatalogStockCurrentQuantity,
                            Request.CatalogStockMinimumQuantity,
                            Request.CatalogStockMaximumQuantity,                          
                            Request.RecordOriginID,
                            Request.RecordStateID,
                            DateTime.Now,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );   

                  await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogStockVerifyFieldsRepository.VerifyFieldsAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogStock.OK)
                {
                    int RecordAffected = await CatalogStockCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {                         
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogStockID                        
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
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGSTOCK_VARIANT);                     
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
