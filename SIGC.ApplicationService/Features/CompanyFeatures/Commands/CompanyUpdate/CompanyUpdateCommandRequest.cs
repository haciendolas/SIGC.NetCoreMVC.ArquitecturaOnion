using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyUpdate
{
    public class CompanyUpdateCommandRequest : IRequest<MsgResponse<object?>>
    {    
        public int CompanyID { get; set; }
        public string CompanyTradeName { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public string CompanyDocumentNumber { get; set; } = null!;
        public DateTime CompanyBirthDate { get; set; }
        public int CountryID { get; set; }
        public string? CompanyAddress { get; set; }
        public Int16 TaxpayerTypeID { get; set; }
        public  short RubroID { get; set; }
        public  string? CompanyCorporateEmail { get; set; }
        public  string? CompanyMobile { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyLogo { get; set; }
        public StateEnum StateID { get; set; }
        public IFileDataService? File { get; set; }
    }   
}