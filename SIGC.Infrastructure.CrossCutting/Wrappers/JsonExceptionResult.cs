namespace SIGC.Infrastructure.CrossCutting.Wrappers;

public class JsonExceptionResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Warning { get; set; }
}