using Newtonsoft.Json;
using SIGC.DomainService.IServices;

namespace SIGC.Infrastructure.GeneralService.Services
{
   internal class JsonSerializerService: IJsonSerializerService
    {
        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);

        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}