using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Catalog;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogGet
{
    internal class CatalogGetQueryHandler : IRequestHandler<CatalogGetQueryRequest, MsgResponse<CatalogGetResponseDto?>>
    {
        private readonly FileUploadSettings FileUploadSettings;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IFileStorageService FileStorageService;
        private readonly ICatalogGetRepository CatalogGetRepository;

        public CatalogGetQueryHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            ICatalogGetRepository CatalogGetRepository
        )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;         
            this.CatalogGetRepository = CatalogGetRepository;
        }
        public async Task<MsgResponse<CatalogGetResponseDto?>> Handle(CatalogGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<CatalogGetResponseDto?>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            var CatalogGet = await CatalogGetRepository.GetAsync(CurrentSessionService.CompanyID, Request.CatalogID, CancellationToken);
            if (CatalogGet is null)
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            else
            {
                FileEntryDto FileEntry = new FileEntryDto(CatalogGet.CatalogImage, $"{FileUploadSettings.CatalogImageLocation}/{CatalogGet.CatalogImage}");
                MsgResponse.Data = new CatalogGetResponseDto(
                
                    CatalogID : CatalogGet.CatalogID,
                    CatalogTypeID: CatalogGet.CatalogTypeID,
                    CategoryID: CatalogGet.CategoryID,
                    CatalogCode: CatalogGet.CatalogCode,
                    CatalogSlug: CatalogGet.CatalogSlug,
                    CatalogName: CatalogGet.CatalogName,
                    SaleConditionID: CatalogGet.SaleConditionID,
                    ManufacturerID: CatalogGet.ManufacturerID,
                    BrandID: CatalogGet.BrandID,
                    PharmaceuticalFormID: CatalogGet.PharmaceuticalFormID,
                    CatalogBrandType: CatalogGet.CatalogBrandType,
                    CatalogConcentration: CatalogGet.CatalogConcentration,
                    CatalogHasVariants: CatalogGet.CatalogHasVariants,
                    CatalogDescription: CatalogGet.CatalogDescription,
                    CatalogImage: CatalogGet.CatalogImage,
                    RecordStateID : CatalogGet.RecordStateID,
                    CatalogUrl : string.IsNullOrWhiteSpace(CatalogGet.CatalogImage) ? "" : FileStorageService.GetFileUrl(FileEntry),
                    TherapeuticActionIDs: CatalogGet.TherapeuticActionIDs,
                    ActiveIngredientIDs: CatalogGet.ActiveIngredientIDs
                );
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            }
            return MsgResponse;
        }
    }
}
