namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public interface IApiServiceFactory
    {
        ApiService Create(string httpClientNamed);
    }
}
