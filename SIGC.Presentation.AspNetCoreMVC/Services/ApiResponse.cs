namespace SIGC.Presentation.AspNetCoreMVC.Services
{   
   public class ApiResponse<T>
    {
        public string Type { get; set; } = null!;
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}