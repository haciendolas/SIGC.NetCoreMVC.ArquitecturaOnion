using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyCreate
{
    public class CompanyCreateCommandRequest : IRequest<MsgResponse<object?>>
    {       
        public string CompanyTradeName { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public string CompanyDocumentNumber { get; set; } = null!;
        public DateTime CompanyBirthDate { get; set; }
        public int CountryID { get; set; }
        public string? CompanyAddress { get; set; }
        public short TaxpayerTypeID { get; set; }
        public short RubroID { get; set; }
        public string? CompanyCorporateEmail { get; set; }
        public string? CompanyMobile { get; set; }
        public string? CompanyPhone { get; set; }    
        public StateEnum StateID { get; set; }
        public IFileDataService? File { get; set; }
       //public List<IFileDataService>? Files { get; set; } 
    }
}