namespace SIGC.ApplicationService.Commons.Dtos;

public sealed class PaginationResultDto<T> where T : class
{
    public int Count { get; set; }
    public IEnumerable<T> Entities { get; set; } = null!;
}