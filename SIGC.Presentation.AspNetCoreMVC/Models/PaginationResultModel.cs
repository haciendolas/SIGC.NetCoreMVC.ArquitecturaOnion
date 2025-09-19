namespace SIGC.Presentation.AspNetCoreMVC.Models;

public sealed class PaginationResultModel<T> where T : class
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
    public List<T> Items { get; set; } = new List<T>();
}