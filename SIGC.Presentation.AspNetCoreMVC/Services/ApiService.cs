using Microsoft.AspNetCore.WebUtilities;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public class ApiService: IApiService
    {
        private readonly HttpClient HttpClient;

        public ApiService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }

        // ========== GET ==========
        public async Task<T> GetAsync<T>(string endpoint, object? queryParams = null)
        {
            if (queryParams is not null)
            {
                var queryDict = ConvertsHelper.GetQueryParams(queryParams);
                endpoint = QueryHelpers.AddQueryString(endpoint, queryDict!);
            }

            var response = await HttpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // ========== POST (JSON) ==========
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest? body, object? queryParams = null)
        {
            HttpContent? jsonContent = null;
            if(body is not null)
               jsonContent = new StringContent(JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json");

            if (queryParams is not null)
            {
                var queryDict = ConvertsHelper.GetQueryParams(queryParams);
                endpoint = QueryHelpers.AddQueryString(endpoint, queryDict);
            }

            var response = await HttpClient.PostAsync(endpoint, jsonContent);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // ========== PUT (JSON) ==========
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest? body, object? queryParams = null)
        {
            HttpContent? jsonContent = null;
            if (body is not null)
                jsonContent = new StringContent(JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json");

            if (queryParams is not null)
            {
                var queryDict = ConvertsHelper.GetQueryParams(queryParams);
                endpoint = QueryHelpers.AddQueryString(endpoint, queryDict);
            }

            var response = await HttpClient.PutAsync(endpoint, jsonContent);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // ========== DELETE (JSON)==========
        public async Task<TResponse> DeleteAsync<TResponse>(string endpoint)
        {
            var response = await HttpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // ========== POST Multipart/Form-Data (genérico) ==========
        public async Task<TResponse> PostFormDataAsync<TRequest, TResponse>(
           string endpoint,
           TRequest dataObject,
           Dictionary<string, (Stream Stream, string FileName, string ContentType)> files
       )
        {
            using var form = new MultipartFormDataContent();

            var props = typeof(TRequest).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var value = prop.GetValue(dataObject);
                if (value != null)
                {
                    form.Add(new StringContent(value.ToString()), prop.Name);
                }
            }

            foreach (var file in files)
            {
                var (stream, fileName, contentType) = file.Value;
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                form.Add(fileContent, file.Key, fileName);
            }

            var response = await HttpClient.PostAsync(endpoint, form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
