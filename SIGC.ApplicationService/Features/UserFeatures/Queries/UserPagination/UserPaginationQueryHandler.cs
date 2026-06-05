using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.User;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserFeatures.Queries.UserPagination
{
    internal class UserPaginationQueryHandler : IRequestHandler<UserPaginationQueryRequest, MsgResponse<PaginationResultDto<UserPaginationQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly IUserPaginationRepository UserPaginationRepository;
        public UserPaginationQueryHandler(
            IMessageService MessageService,
            IUserPaginationRepository UserPaginationRepository)
        {
            this.MessageService = MessageService;
            this.UserPaginationRepository = UserPaginationRepository;
        }

        public async Task<MsgResponse<PaginationResultDto<UserPaginationQueryResponse>>> Handle(UserPaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<UserPaginationQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var Response = await UserPaginationRepository.PaginationAsync(new UserPaginationRequestDto
            {
                CompanyID = Request.CompanyID,
                UserFullName = Request.UserFullName,          
                StateID = Request.StateID,
                Parameters = new PaginationParametersDto()
                {
                    Search = Request.Search ?? "",
                    PageNumber = Request.PageNumber,
                    PageSize = Request.PageSize
                }
            }, CancellationToken);

            if (!Response.Entities.Any()) MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            MsgResponse.Data = new PaginationResultDto<UserPaginationQueryResponse>();
            MsgResponse.Data.Items = Response.Entities.Select(s => new UserPaginationQueryResponse{
                                        UserID = s.UserID,
                                        UserFirstName = s.UserFirstName,
                                        UserLastName = s.UserLastName,
                                        UserName = s.UserName,
                                        UserMail = s.UserMail,
                                        StateID = s.StateID,
                                        UserRolNames = s.UserRolNames,
                                        UserLastUpdatedDateTime = s.UserLastUpdatedDateTime,
                                        UserLastUpdatedUserName = s.UserLastUpdatedUserName
                                    }).ToList();
            MsgResponse.Data.TotalRecords = Response.Total;
            MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;
        }
    }
}