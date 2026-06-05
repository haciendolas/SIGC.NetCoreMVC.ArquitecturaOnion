namespace SIGC.DomainModel.Dtos.Establishment
{
    public sealed record EstablishmentGetResponseDto
    (
        int EstablishmentID,
        byte TypeID,
        string EstablishmentCode,
        string EstablishmentName,
        string EstablishmentAddress,
        string EstablishmentLogo,
        byte RecordStateID,
        string? EstablishmentUrl
    );
}