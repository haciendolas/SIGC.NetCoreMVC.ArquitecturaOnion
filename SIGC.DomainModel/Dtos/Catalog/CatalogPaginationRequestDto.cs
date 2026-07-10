using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Catalog
{
    public sealed record CatalogPaginationRequestDto
    ( int CompanyID,
      byte? CatalogTypeID,
      byte? RecordStateID,
      int? CategoryID,
      int? ManufacturerID,
      int? BrandID,
      PaginationParametersDto Parameters
    );    
}