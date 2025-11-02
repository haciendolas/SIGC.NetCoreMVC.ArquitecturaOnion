namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Ubigeo
{
    public record struct UbigeoListByClassAndCodeAndLenCodeResponseModel
    (
         int UbigeoID,
         string UbigeoCode,
         string UbigeoName
    );
}