namespace SIGC.DomainModel.Dtos.Constant
{
   public record struct ConstantListResponseDto
   (
       short ConstantID,
       int ConstantClass,
       string ConstantAbbreviation,
       string ConstantName
   );
}