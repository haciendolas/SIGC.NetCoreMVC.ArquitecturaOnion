namespace SIGC.DomainModel.Dtos.Establishment
{
    public sealed record EstablishmentListResponseDto
    (
       int EstablishmentID,
       string EstablishmentName,
       string EstablishmentAddress
    );
}