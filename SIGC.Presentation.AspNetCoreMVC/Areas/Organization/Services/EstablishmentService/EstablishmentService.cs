using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.EstablishmentService
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Establishment";

        public EstablishmentService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
        public async Task<ApiResponse<object?>> EstablishmentCreate(EstablishmentCreateUpdateRequestModel Request)
        {
            return await ApiService.PostFormDataAsync<EstablishmentCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/EstablishmentCreate", Request);
        }
        public async Task<ApiResponse<object?>> EstablishmentUpdate(EstablishmentCreateUpdateRequestModel Request)
        {
            return await ApiService.PutFormDataAsync<EstablishmentCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/EstablishmentUpdate", Request);
        }
        public async Task<ApiResponse<object?>> EstablishmentChangeState(EstablishmentChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<EstablishmentChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/EstablishmentChangeState", Request);
        }
        public async Task<ApiResponse<EstablishmentGetResponseModel?>> EstablishmentGet(int EstablishmentID)
        {
            return await ApiService.GetAsync<ApiResponse<EstablishmentGetResponseModel?>>($"{Controller}/EstablishmentGet/{EstablishmentID}");
        }
        public async Task<ApiResponse<PaginationResultModel<EstablishmentPaginationResponseModel>>> EstablishmentPagination(EstablishmentPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<EstablishmentPaginationResponseModel>>>($"{Controller}/EstablishmentPagination", null, Request);
        }
    }
}