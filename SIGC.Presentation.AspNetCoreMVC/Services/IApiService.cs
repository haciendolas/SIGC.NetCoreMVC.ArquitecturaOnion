namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public interface IApiService
    {      
        Task<T> GetAsync<T>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse> DeleteAsync<TResponse>(string endpoint);

        Task<TResponse> PostFormDataAsync<TRequest, TResponse>(
            string endpoint,
            TRequest dataObject,
            Dictionary<string, (Stream Stream, string FileName, string ContentType)> files
        );
    }
}