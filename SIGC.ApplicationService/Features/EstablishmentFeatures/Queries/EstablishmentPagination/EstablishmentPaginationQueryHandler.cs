using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentPagination
{
    internal class EstablishmentPaginationQueryHandler : IRequestHandler<EstablishmentPaginationQueryRequest, MsgResponse<PaginationResultDto<EstablishmentPaginationQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IEstablishmentPaginationRepository EstablishmentPaginationRepository;

        public EstablishmentPaginationQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IEstablishmentPaginationRepository EstablishmentPaginationRepository)
        {
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.EstablishmentPaginationRepository = EstablishmentPaginationRepository;
        }

        public async Task<MsgResponse<PaginationResultDto<EstablishmentPaginationQueryResponse>>> Handle(EstablishmentPaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<EstablishmentPaginationQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var Response = await EstablishmentPaginationRepository.PaginationAsync(new EstablishmentPaginationRequestDto
            (
                PersonID : Request.PersonID.HasValue ? Request.PersonID.Value : CurrentSessionService.CompanyID,
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

            MsgResponse.Data = new PaginationResultDto<EstablishmentPaginationQueryResponse>();
            MsgResponse.Data.Items = Response.Entities.Select(s => new EstablishmentPaginationQueryResponse
            {
                EstablishmentID = s.EstablishmentID,
                EstablishmentCode = s.EstablishmentCode,
                EstablishmentName = s.EstablishmentName,
                EstablishmentAddress = s.EstablishmentAddress,
                RecordStateID = s.RecordStateID,
                EstablishmentLastUpdatedDateTime = s.EstablishmentLastUpdatedDateTime,
                EstablishmentLastUpdatedUserID = s.EstablishmentLastUpdatedUserID,
                EstablishmentLastUpdatedUserName = s.EstablishmentLastUpdatedUserName,
                EstablishmentLastUpdatedUserFullName = s.EstablishmentLastUpdatedUserFullName,
            }).ToList();
            MsgResponse.Data.TotalRecords = Response.Total;
            MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;
        }
    }
}
