using Microsoft.AspNetCore.StaticFiles;
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
           TRequest dataObject           
       )
        {
            using var form = new MultipartFormDataContent();
            var props = typeof(TRequest).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var provider = new FileExtensionContentTypeProvider();

            foreach (var prop in props)
            {
                var value = prop.GetValue(dataObject);
                if (value == null) continue;

                var type = prop.PropertyType;

                // Manejo DateTime
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    form.Add(new StringContent(((DateTime)value).ToString("yyyy-MM-dd")), prop.Name);
                    continue;
                }

                // Manejo bool
                if (type == typeof(bool) || type == typeof(bool?))
                {
                    form.Add(new StringContent(((bool)value) ? "true" : "false"), prop.Name);
                    continue;
                }

                // Manejo IFormFile
                if (value is IFormFile singleFile)
                {
                    AddFileToForm(form, singleFile, prop.Name, provider);
                    continue;
                }

                // Manejo IEnumerable<IFormFile>
                if (value is IEnumerable<IFormFile> fileCollection)
                {
                    foreach (var file in fileCollection)
                    {
                        AddFileToForm(form, file, prop.Name, provider);
                    }
                    continue;
                }

                // Manejo Stream
                if (value is Stream stream)
                {
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    form.Add(fileContent, prop.Name, prop.Name + ".bin");
                    continue;
                }

                // Manejo byte[]
                if (value is byte[] bytes)
                {
                    var fileContent = new ByteArrayContent(bytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    form.Add(fileContent, prop.Name, prop.Name + ".bin");
                    continue;
                }

                // Propiedades normales (string, int, etc.)
                form.Add(new StringContent(value.ToString()!), prop.Name);
            }

            var response = await HttpClient.PostAsync(endpoint, form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;


        }

        // Método auxiliar para agregar IFormFile al form
        private void AddFileToForm(MultipartFormDataContent form, IFormFile file, string propName, FileExtensionContentTypeProvider provider)
        {
            var fileContent = new StreamContent(file.OpenReadStream());

            // Detectar Content-Type usando la extensión
            if (!provider.TryGetContentType(file.FileName, out var contentType))
            {
                contentType = file.ContentType ?? "application/octet-stream";
            }

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            form.Add(fileContent, propName, file.FileName);
        }
    }
}
