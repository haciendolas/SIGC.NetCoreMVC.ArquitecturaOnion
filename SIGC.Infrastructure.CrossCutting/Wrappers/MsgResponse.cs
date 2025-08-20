namespace SIGC.Infrastructure.CrossCutting.Wrappers;

public class MsgResponse<TData>
{
    public MsgResponse()
    {
    }

    public MsgResponse(TData data)
    {
        Data = data;
    }

    public MsgResponse(TData data, string message)
    {
        Data = data;
        Message = message;
    }

    public MsgResponse(TData data, string type,string message)
    {
        Data = data;
        Type = type;      
        Message = message;
    }

    public MsgResponse(string type, string message, TData? data = default)
    {
        Type = type;
        Message = message;
        Data = data;
    }

    public string Type { get; set; }
    public string Message { get; set; }
    public TData? Data { get; set; }
}