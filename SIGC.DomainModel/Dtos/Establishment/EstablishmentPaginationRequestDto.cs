using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Establishment
{ 
    public sealed record EstablishmentPaginationRequestDto(
      int CompanyID,
      int PersonID,
      byte? RecordStateID,
      PaginationParametersDto Parameters
  );
}
