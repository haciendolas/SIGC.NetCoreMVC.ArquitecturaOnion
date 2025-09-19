namespace SIGC.ApplicationService.Commons.Dtos;

public sealed class PaginationResultDto<T> where T : class
{
    /// <summary>
    /// Total de registros sin filtrar
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Total de registros después de aplicar filtros
    /// </summary>
    public int RecordsFiltered { get; set; }

    /// <summary>
    /// Lista de registros en la página actual
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();
}