using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyUpdate
{
    public record struct CompanyUpdateCommandRequest
    (
      int CompanyID ,
      string CompanyTradeName,
      string CompanySocialReason,
      string CompanyDocumentNumber,
      DateTime CompanyBirthDate,
      int CountryID,
      string? CompanyAddress,
      Int16 TaxpayerTypeID ,
      short RubroID,
      string? CompanyCorporateEmail,
      string? CompanyMobile ,
      string? CompanyPhone,
      string? CompanyLogo ,
      StateEnum StateID ,
      DateTime CreatedDateTime,
      int CreatedBy
    ):IRequest<MsgResponse<object?>>;    
}