namespace SIGC.DomainService.IServices
{   
    public interface IFileDataService
    {
        string FileName { get; }
        string ContentType { get; }
        Stream OpenReadStream();
    }
}