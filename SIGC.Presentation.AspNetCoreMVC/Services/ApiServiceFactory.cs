namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public class ApiServiceFactory : IApiServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiServiceFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ApiService Create(string httpClientNamed)
        {
            var client = _httpClientFactory.CreateClient(httpClientNamed);
            return new ApiService(client);
        }
    }
}
