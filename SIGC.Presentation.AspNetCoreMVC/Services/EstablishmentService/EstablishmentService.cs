using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.Establishment;

namespace SIGC.Presentation.AspNetCoreMVC.Services.EstablishmentService
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Establishment";

        public EstablishmentService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<EstablishmentListResponseModel>>> EstablishmentList(int PersonID)
        {
            return await ApiService.GetAsync<ApiResponse<List<EstablishmentListResponseModel>>>($"{Controller}/EstablishmentList/{PersonID}");
        }     
    }
}