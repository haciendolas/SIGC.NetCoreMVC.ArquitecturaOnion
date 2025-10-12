using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyPagination
{
    internal class CompanyPaginationQueryHandler : IRequestHandler<CompanyPaginationQueryRequest, MsgResponse<PaginationResultDto<CompanyPaginationQueryResponse>>>
    {
        private readonly ICompanyPaginationRepository CompanyPaginationRepository;
        private readonly IMessageService MessageService;
        public CompanyPaginationQueryHandler(ICompanyPaginationRepository CompanyPaginationRepository,
            IMessageService MessageService
            )
        {
            this.CompanyPaginationRepository = CompanyPaginationRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<PaginationResultDto<CompanyPaginationQueryResponse>>> Handle(CompanyPaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<CompanyPaginationQueryResponse>>();
                MsgResponse.Type = MessageTypeConst.QUERY;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
               var Response = await CompanyPaginationRepository.PaginationAsync(new CompanyPaginationResquestDto{
                                        CompanyIDRegister = Request.CompanyIDRegister,
                                       TaxpayerTypeID = Request.TaxpayerTypeID,
                                       SectorID = Request.SectorID,
                                       CompanyDocumentNumber = Request.CompanyDocumentNumber,
                                       CompanySocialReason = Request.CompanySocialReason,
                                        StateID = Request.StateID,
                                        Parameters  = new PaginationParametersDto(){
                                            Search =Request.Search ?? "",
                                            PageNumber = Request.PageNumber,
                                            PageSize = Request.PageSize
                                        }
                                    }, CancellationToken);

               if(!Response.Entities.Any()) MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

               MsgResponse.Data = new PaginationResultDto<CompanyPaginationQueryResponse>();
               MsgResponse.Data.Items  = Response.Entities.Select(s=> new CompanyPaginationQueryResponse{
                                       CompanyID = s.CompanyID,
                                       TaxpayerTypeName = s.TaxpayerTypeName,
                                       CompanyDocumentNumber = s.CompanyDocumentNumber,
                                       CompanySocialReason = s.CompanySocialReason,
                                       SectorName = s.SectorName,
                                       CountryName =s.CountryName,
                                       StateID = s.StateID,       
                                       CompanyLastUpdatedDateTime = s.CompanyLastUpdatedDateTime,
                                       CompanyLastUpdatedUserName = s.CompanyLastUpdatedUserName
                                   }).ToList();
                MsgResponse.Data.TotalRecords = Response.Total;
                MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;

        }
    }
}