using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Catalog;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogPagination
{
    internal class CatalogPaginationQueryHandler : IRequestHandler<CatalogPaginationQueryRequest, MsgResponse<PaginationResultDto<CatalogPaginationQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogPaginationRepository CatalogPaginationRepository;

        public CatalogPaginationQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogPaginationRepository CatalogPaginationRepository)
        {
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogPaginationRepository = CatalogPaginationRepository;
        }

        public async Task<MsgResponse<PaginationResultDto<CatalogPaginationQueryResponse>>> Handle(CatalogPaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<CatalogPaginationQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT); 
            var Response = await CatalogPaginationRepository.PaginationAsync(new CatalogPaginationRequestDto
            ( 
                CompanyID: CurrentSessionService.CompanyID,
                CatalogTypeID : Request.CatalogTypeID,             
                RecordStateID: Request.RecordStateID,
                CategoryID: Request.CategoryID, 
                ManufacturerID: Request.ManufacturerID,
                BrandID: Request.BrandID,
                Parameters: new PaginationParametersDto()
                {
                    Search = Request.Search ?? "",
                    PageNumber = Request.PageNumber,
                    PageSize = Request.PageSize
                }
            ), CancellationToken);

            if (!Response.Entities.Any()) MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            MsgResponse.Data = new PaginationResultDto<CatalogPaginationQueryResponse>();
            MsgResponse.Data.Items = Response.Entities.Select(s => new CatalogPaginationQueryResponse
            {
                CatalogID = s.CatalogID,
                CatalogName = s.CatalogName,
                CatalogDescription = s.CatalogDescription,
                CatalogTypeName = s.CatalogTypeName,
                CategoryName  = s.CategoryName,
                CatalogVariantName = s.CatalogVariantName,
                UnitMeasureName = s.UnitMeasureName,
                PresentationName = s.PresentationName,
                BrandName = s.BrandName,
                ManufacturerName = s.ManufacturerName,
                ActiveIngredient = s.ActiveIngredient,
                TherapeuticAction = s.TherapeuticAction,
                RecordStateID = s.RecordStateID,
                CatalogLastUpdatedDateTime = s.CatalogLastUpdatedDateTime,        
                CatalogLastUpdatedUserName = s.CatalogLastUpdatedUserName              
            }).ToList();
            MsgResponse.Data.TotalRecords = Response.Total;
            MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;
        }
    }
}
