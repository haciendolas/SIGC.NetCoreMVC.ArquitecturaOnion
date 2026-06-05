using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.EstablishmentService
{
    public interface IEstablishmentService
    {
        Task<ApiResponse<object?>> EstablishmentCreate(EstablishmentCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> EstablishmentUpdate(EstablishmentCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> EstablishmentChangeState(EstablishmentChangeStateRequestModel Request);
        Task<ApiResponse<EstablishmentGetResponseModel?>> EstablishmentGet(int EstablishmentID);
        Task<ApiResponse<PaginationResultModel<EstablishmentPaginationResponseModel>>> EstablishmentPagination(EstablishmentPaginationRequestModel Request);
    }
}