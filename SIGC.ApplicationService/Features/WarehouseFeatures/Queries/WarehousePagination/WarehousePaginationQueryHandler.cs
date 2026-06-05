using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehousePagination
{
    internal class WarehousePaginationQueryHandler : IRequestHandler<WarehousePaginationQueryRequest, MsgResponse<PaginationResultDto<WarehousePaginationQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IWarehousePaginationRepository WarehousePaginationRepository;

        public WarehousePaginationQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IWarehousePaginationRepository WarehousePaginationRepository)
        {
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.WarehousePaginationRepository = WarehousePaginationRepository;
        }

        public async Task<MsgResponse<PaginationResultDto<WarehousePaginationQueryResponse>>> Handle(WarehousePaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<WarehousePaginationQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var Response = await WarehousePaginationRepository.PaginationAsync(new WarehousePaginationRequestDto
            (
                EstablishmentID : Request.EstablishmentID,
                CompanyID: CurrentSessionService.CompanyID,
                RecordStateID: Request.RecordStateID,
                Parameters: new PaginationParametersDto()
                {
                    Search = Request.Search ?? "",
                    PageNumber = Request.PageNumber,
                    PageSize = Request.PageSize
                }
            ), CancellationToken);

            if (!Response.Entities.Any()) MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            MsgResponse.Data = new PaginationResultDto<WarehousePaginationQueryResponse>();
            MsgResponse.Data.Items = Response.Entities.Select(s => new WarehousePaginationQueryResponse
            {
                WarehouseID = s.WarehouseID,
                WarehouseCode = s.WarehouseCode,
                WarehouseName = s.WarehouseName,
                EstablishmentCodeAndName = $"{s.EstablishmentCode}-{s.EstablishmentName}",
                RecordStateID = s.RecordStateID,
                WarehouseLastUpdatedDateTime = s.WarehouseLastUpdatedDateTime,
                WarehouseLastUpdatedUserID = s.WarehouseLastUpdatedUserID,
                WarehouseLastUpdatedUserName = s.WarehouseLastUpdatedUserName,
                WarehouseLastUpdatedUserFullName = s.WarehouseLastUpdatedUserFullName,
            }).ToList();
            MsgResponse.Data.TotalRecords = Response.Total;
            MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;
        }
    }
}
