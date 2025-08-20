namespace SIGC.ApplicationService.Commons.Dtos;

public class PaginationParametersDto
{
    public string? SortField { get; set; }
    //public SortTypeEnum SortType { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}