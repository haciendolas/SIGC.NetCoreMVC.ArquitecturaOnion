using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIGC.DomainModel.Dtos;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIGC.Infrastructure.GeneralService.Services
{
    internal class GenerateJWTToken : IGenerateJWTToken
    {
        readonly IConfiguration Configuration;

        public GenerateJWTToken(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        Task<string> IGenerateJWTToken.GenerateJWTToken(AppUserDto AppUser)
        {
            SigningCredentials SigningCredentials = GetSigningCredentials();

            List<Claim> UserClaims = GetClaims(AppUser);

            JwtSecurityToken TokenOptions = GetTokenOptions(SigningCredentials, UserClaims);

            string Token = new JwtSecurityTokenHandler().WriteToken(TokenOptions);

            return Task.FromResult(Token);
        }

        private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> userClaims)
        {
            return new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["JWT:ExpiredInMinutes"])),
                signingCredentials: signingCredentials
                );
        }

        private List<Claim> GetClaims(AppUserDto AppUser)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, AppUser.UserID.ToString()),
                new Claim(ClaimTypes.Name, AppUser.UserName),
                new Claim(ClaimTypes.Role, AppUser.RoleCodes),
                new Claim(CustomClaimTypes.COMPANY_ID, AppUser.CompanyID.ToString()),
                new Claim(CustomClaimTypes.IDIOM_ID, AppUser.IdiomID.ToString()),
                new Claim(CustomClaimTypes.COMPANY_DOCUMENTNUMBER, AppUser.CompanyDocumentNumber),
                new Claim(CustomClaimTypes.COMPANY_SOCIALREASON, AppUser.CompanySocialReason),
                new Claim(CustomClaimTypes.COMPANY_TRADENAME, AppUser.CompanyTradeName)
            };
        }

        private SigningCredentials GetSigningCredentials()
        {
            var Key = Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]);
            var Secret = new SymmetricSecurityKey(Key);

            return new SigningCredentials(Secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
