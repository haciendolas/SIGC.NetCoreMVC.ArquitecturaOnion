using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.IRepositories.ICatalogVariantValueRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantCreate
{
    internal class CatalogVariantCreateCommandHandler : IRequestHandler<CatalogVariantCreateCommandRequest, MsgResponse<object?>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICatalogVariantCreateRepository CatalogVariantCreateRepository;
        private readonly ICatalogVariantValueCreateUpdateRepository CatalogVariantValueCreateUpdateRepository;
        private readonly ICatalogPresentationCreateRepository CatalogPresentationCreateRepository;
        private readonly ICatalogVariantVerifySkuAndNameRepository CatalogVariantVerifySkuAndNameRepository;
        public CatalogVariantCreateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IUnitOfWork UnitOfWork,
            ICatalogVariantCreateRepository CatalogVariantCreateRepository,
            ICatalogVariantValueCreateUpdateRepository CatalogVariantValueCreateUpdateRepository,
            ICatalogPresentationCreateRepository CatalogPresentationCreateRepository,
            ICatalogVariantVerifySkuAndNameRepository CatalogVariantVerifySkuAndNameRepository
        )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.UnitOfWork = UnitOfWork;
            this.CatalogVariantCreateRepository = CatalogVariantCreateRepository;
            this.CatalogVariantValueCreateUpdateRepository = CatalogVariantValueCreateUpdateRepository;
            this.CatalogPresentationCreateRepository = CatalogPresentationCreateRepository;
            this.CatalogVariantVerifySkuAndNameRepository = CatalogVariantVerifySkuAndNameRepository;
        }

        public async Task<MsgResponse<object?>> Handle(CatalogVariantCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = CatalogVariant.Create(
                        CurrentSessionService.CompanyID,
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

                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogVariantVerifySkuAndNameRepository.VerifySkuAndNameAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.CatalogVariant.OK)
                {
                    int RecordAffected = await CatalogVariantCreateRepository.CreateAsync(Model, CancellationToken);
                    foreach (var CatalogVariantValue in Request.CatalogVariantValues)
                    {
                        Model.AddCatalogVariantValue(CatalogVariantValue.AttributeValueID);
                    }
                    foreach (var CatalogVariantValue in Model.CatalogVariantValues)
                    {
                        await CatalogVariantValueCreateUpdateRepository.CreateUpdateAsync(CatalogVariantValue, CancellationToken);
                    }

                    foreach (var CatalogPresentationItem in Request.CatalogPresentations)
                    {
                        var CatalogPresentationModel = CatalogPresentation.Create(
                            CurrentSessionService.CompanyID,
                            Model.CatalogVariantID,
                            CatalogPresentationItem.PresentationID,
                            CatalogPresentationItem.CatalogPresentationIsDefault,
                            CatalogPresentationItem.CatalogPresentationEquivalence,
                            CatalogPresentationItem.CatalogPresentationSKU,
                            CatalogPresentationItem.CatalogPresentationBarcode,
                            Request.RecordOriginID,
                            CatalogPresentationItem.RecordStateID,
                            CurrentDate,
                            CurrentSessionService.UserID,
                            CurrentSessionService.UserName,
                            CurrentSessionService.UserFullName
                        );
                        await CatalogPresentationCreateRepository.CreateAsync(CatalogPresentationModel, CancellationToken);
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