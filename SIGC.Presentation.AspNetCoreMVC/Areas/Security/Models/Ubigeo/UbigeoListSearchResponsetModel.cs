namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Ubigeo
{
    public record struct UbigeoListSearchResponsetModel
    (
        string DepartmentName,
        string ProvinceName,
        int DistrictID,
        string DistrictCode,
        string DistrictName
    );
}