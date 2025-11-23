namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public interface IApiService
    {      
        Task<T> GetAsync<T>(string endpoint, object? queryParams = null);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest? body,object? queryParams = null);
        Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest? body, object? queryParams = null);
        Task<TResponse> DeleteAsync<TResponse>(string endpoint);
        Task<TResponse> PostFormDataAsync<TRequest, TResponse>(string endpoint,TRequest dataObject);
        Task<TResponse> PutFormDataAsync<TRequest, TResponse>(string endpoint, TRequest dataObject);
    }
}