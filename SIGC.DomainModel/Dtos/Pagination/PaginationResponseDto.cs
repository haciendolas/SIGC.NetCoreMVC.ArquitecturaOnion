namespace SIGC.DomainModel.Dtos.Pagination;

public class PaginationResponseDto<T> where T : class
{
    /// <summary>
    /// Total de registros sin aplicar filtros
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// Total de registros después de aplicar filtros
    /// </summary>
    public int Filtered { get; set; }

    /// <summary>
    /// Lista de registros en la página actual
    /// </summary>
    public List<T> Entities { get; set; } = new List<T>();
}