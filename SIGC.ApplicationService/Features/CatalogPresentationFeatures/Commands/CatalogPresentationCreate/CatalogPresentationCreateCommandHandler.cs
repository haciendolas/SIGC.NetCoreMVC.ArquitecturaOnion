using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Commands.CatalogPresentationCreate
{
    internal class CatalogPresentationCreateCommandHandler : IRequestHandler<CatalogPresentationCreateCommandRequest, MsgResponse<object>>
    {
        private readonly ICatalogPresentationCreateRepository CatalogPresentationCreateRepository;
        private readonly ICatalogPresentationVerifyFieldsRepository CatalogPresentationVerifyFieldsRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork; 

        public CatalogPresentationCreateCommandHandler(
            ICatalogPresentationCreateRepository CatalogPresentationCreateRepository,
            ICatalogPresentationVerifyFieldsRepository CatalogPresentationVerifyFieldsRepository, 
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IUnitOfWork UnitOfWork)
        {
            this.CatalogPresentationCreateRepository = CatalogPresentationCreateRepository;
            this.CatalogPresentationVerifyFieldsRepository = CatalogPresentationVerifyFieldsRepository;
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object>> Handle(CatalogPresentationCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();     
            try
            {
                var Model = CatalogPresentation.Create(
                            CurrentSessionService.CompanyID,
                            Request.CatalogVariantID,
                            Request.PresentationID,
                            Request.CatalogPresentationIsDefault,
                            Request.CatalogPresentationEquivalence,
                            Request.CatalogPresentationSKU,
                            Request.CatalogPresentationBarcode,
                            Request.RecordOriginID,
                            Request.RecordStateID,
                            DateTime.Now,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );

                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogPresentationVerifyFieldsRepository.VerifyFieldsAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogPresentation.OK)
                {
                    int RecordAffected = await CatalogPresentationCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {                         
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogPresentationID,
                            Model.CatalogPresentationSKU,                    
                            Model.CreatedDate,
                        }; 
                    }
                    else
                    {
                        await UnitOfWork.RollbackTransactionAsync(CancellationToken); 
                      
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else{
                    await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                    if (Verify == VerifyRegistryConst.CatalogPresentation.SKU_EXISTS) {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGPRESENTATION_SKU);
                    }
                    else if(Verify == VerifyRegistryConst.CatalogPresentation.SKU_AND_BARCODE_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGPRESENTATION_SKU_AND_BARCODE);
                    }
                    else if (Verify == VerifyRegistryConst.CatalogPresentation.BARCODE_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGPRESENTATION_BARCODE);
                    }                 
                    else{
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGPRESENTATION_PRESENTATION);
                    };
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
