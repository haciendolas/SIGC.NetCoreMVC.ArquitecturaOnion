using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Warehouse
{ 
    public sealed record WarehousePaginationRequestDto(
      int CompanyID,
      int EstablishmentID,
      byte? RecordStateID,
      PaginationParametersDto Parameters
  );
}
