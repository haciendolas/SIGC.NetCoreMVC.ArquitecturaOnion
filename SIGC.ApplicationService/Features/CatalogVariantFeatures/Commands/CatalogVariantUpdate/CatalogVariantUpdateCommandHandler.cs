using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.IRepositories.ICatalogVariantValueRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantUpdate
{
    internal class CatalogVariantUpdateCommandHandler : IRequestHandler<CatalogVariantUpdateCommandRequest, MsgResponse<object?>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICatalogVariantUpdateRepository CatalogVariantUpdateRepository;
        private readonly ICatalogVariantValueCreateUpdateRepository CatalogVariantValueCreateUpdateRepository;    
        private readonly ICatalogPresentationUpdateRepository CatalogPresentationUpdateRepository;
        private readonly ICatalogVariantVerifySkuAndNameRepository CatalogVariantVerifySkuAndNameRepository;
        private readonly ICatalogVariantValueDeleteRepository CatalogVariantValueDeleteRepository;
        public CatalogVariantUpdateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IUnitOfWork UnitOfWork,
            ICatalogVariantUpdateRepository CatalogVariantUpdateRepository,
            ICatalogVariantValueCreateUpdateRepository CatalogVariantValueCreateUpdateRepository,
            ICatalogPresentationCreateRepository CatalogPresentationCreateRepository,
            ICatalogVariantVerifySkuAndNameRepository CatalogVariantVerifySkuAndNameRepository,
            ICatalogVariantValueDeleteRepository CatalogVariantValueDeleteRepository,
            ICatalogPresentationUpdateRepository CatalogPresentationUpdateRepository
        )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.UnitOfWork = UnitOfWork;
            this.CatalogVariantUpdateRepository = CatalogVariantUpdateRepository;
            this.CatalogVariantValueCreateUpdateRepository = CatalogVariantValueCreateUpdateRepository;         
            this.CatalogVariantVerifySkuAndNameRepository = CatalogVariantVerifySkuAndNameRepository;
            this.CatalogVariantValueDeleteRepository = CatalogVariantValueDeleteRepository;
            this.CatalogPresentationUpdateRepository = CatalogPresentationUpdateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(CatalogVariantUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = CatalogVariant.Update(
                        CurrentSessionService.CompanyID,
                        Request.CatalogVariantID,
                        Request.CatalogID,
                        Request.CatalogVariantName,
                        Request.CatalogVariantSKU,    
                        Request.RecordOriginID,
                        Request.RecordStateID,
                        CurrentDate,
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );

                foreach (var CatalogVariantValue in Request.CatalogVariantValues)
                {
                    Model.AddCatalogVariantValue(CatalogVariantValue.AttributeValueID);
                }

                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogVariantVerifySkuAndNameRepository.VerifySkuAndNameAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogVariant.OK)
                {
                    int RecordAffected = await CatalogVariantUpdateRepository.UpdateAsync(Model, CancellationToken);
                       
                       await CatalogVariantValueDeleteRepository.DeleteAsync(Model.CompanyID,Model.CatalogVariantID,Request.CatalogVariantValues.Select(sel=>sel.AttributeValueID).ToList(), CancellationToken);
                  
                    foreach (var CatalogVariantValue in Model.CatalogVariantValues)
                    {
                        await CatalogVariantValueCreateUpdateRepository.CreateUpdateAsync(CatalogVariantValue, CancellationToken);
                    }

                    foreach (var CatalogPresentationItem in Request.CatalogPresentations)
                    { 
                        var CatalogPresentationModel = CatalogPresentation.Update(
                                CurrentSessionService.CompanyID,
                                CatalogPresentationItem.CatalogPresentationID,
                                Model.CatalogVariantID,
                                CatalogPresentationItem.PresentationID,                        
                                CatalogPresentationItem.CatalogPresentationEquivalence,
                                CatalogPresentationItem.CatalogPresentationSKU,
                                CatalogPresentationItem.CatalogPresentationBarcode,
                                CatalogPresentationItem.RecordStateID,
                                CurrentDate,
                                CurrentSessionService.UserID,
                                CurrentSessionService.UserName,
                                CurrentSessionService.UserFullName
                            );
                        await CatalogPresentationUpdateRepository.UpdateAsync(CatalogPresentationModel, CancellationToken);
                    }

                    if (RecordAffected > 0)
                    {
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogVariantID,                           
                            Model.CatalogVariantName,
                            Model.CatalogVariantSKU,
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
                else
                {
                    await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                    if (Verify == VerifyRegistryConst.CatalogVariant.SKU_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGVARIANT_SKU);
                    }
                    else if (Verify == VerifyRegistryConst.Catalog.NAME_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGVARIANT_NAME);
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOGVARIANT_SKU_AND_NAME);
                    }
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