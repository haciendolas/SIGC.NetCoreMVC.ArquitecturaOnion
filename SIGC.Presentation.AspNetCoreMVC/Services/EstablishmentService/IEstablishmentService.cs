using SIGC.Presentation.AspNetCoreMVC.Models.Establishment;

namespace SIGC.Presentation.AspNetCoreMVC.Services.EstablishmentService
{
    public interface IEstablishmentService
    {
        Task<ApiResponse<List<EstablishmentListResponseModel>>> EstablishmentList(int PersonID);
    }
}