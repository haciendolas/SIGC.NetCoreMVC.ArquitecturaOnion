using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Dtos.Pagination;

public class PaginationParameters
{
   // public string? SortField { get; set; }
   // public SortTypeEnum SortType { get; set; }
   // public int PageNumber { get; set; } = 0;
   // public int PageSize { get; set; } = 10;

    /// <summary>
    /// Campo por el que se ordena la tabla (ej: "Nombre", "Fecha")
    /// </summary>
    public string? SortField { get; set; }

    /// <summary>
    /// Dirección de ordenamiento: Ascendente o Descendente
    /// </summary>
    public SortTypeEnum SortType { get; set; } = SortTypeEnum.Ascending;

    /// <summary>
    /// Número de página actual (comienza en 0 o 1, según tu convención)
    /// </summary>
    public int PageNumber { get; set; } = 0;

    /// <summary>
    /// Cantidad de registros por página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Filtro de búsqueda general (opcional)
    /// </summary>
    public string? Search { get; set; }
}