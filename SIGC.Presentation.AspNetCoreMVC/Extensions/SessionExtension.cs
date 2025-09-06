using Newtonsoft.Json;

namespace SIGC.Presentation.AspNetCoreMVC.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            string data = session.GetString(key);
            if (data is null)
                return default;
            return JsonConvert.DeserializeObject<T>(data)!;            
        } 
 
    }
}
