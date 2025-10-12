using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyCreate
{
    public record struct CompanyCreateCommandRequest
    (      
      string CompanyTradeName,
      string CompanySocialReason,
      string CompanyDocumentNumber,
      DateTime CompanyBirthDate,
      int CountryID,
      string? CompanyAddress,
      Int16 TaxpayerTypeID ,
      short SectorID ,
      string? CompanyMobile ,
      string? CompanyPhone,
      string? CompanyLogo ,
      StateEnum StateID ,
      DateTime CreatedDateTime,
      int CreatedBy
    ):IRequest<MsgResponse<object?>>;    
}