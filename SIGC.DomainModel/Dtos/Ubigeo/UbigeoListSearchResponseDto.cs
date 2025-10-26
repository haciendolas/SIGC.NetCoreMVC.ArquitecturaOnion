namespace SIGC.DomainModel.Dtos.Ubigeo
{
    public record struct UbigeoListSearchResponseDto
    (
        string DepartmentName,
        string ProvinceName,
        int DistrictID,
        string DistrictCode,
        string DistrictName
    );
}