using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIGC.DomainModel.Dtos;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SIGC.Infrastructure.GeneralService.Services
{
    internal class GenerateTokenService : IGenerateTokenService
    {
        private readonly IConfiguration Configuration;
        private readonly SymmetricSecurityKey SymmetricSecurityKey;
        private readonly DateTime CurrentDateTime = DateTime.Now;

        public GenerateTokenService(IConfiguration configuration)
        {
            Configuration = configuration;
            var Key = Encoding.UTF8.GetBytes(Configuration["JWTToken:SecurityKey"]!);
            SymmetricSecurityKey = new SymmetricSecurityKey(Key);
        }

        public Task<string> GenerateJWTToken(AppUserDto AppUser)
        {
            AppUser.CurrentDateTime = CurrentDateTime;
            AppUser.ExpirationJWTDateTime = CurrentDateTime.AddMinutes(Convert.ToDouble(Configuration["JWTToken:ExpiredInMinutes"]));
            AppUser.ExpirationRandomDateTime = CurrentDateTime.AddMinutes(Convert.ToDouble(Configuration["RandomToken:ExpiredInMinutes"]));
            
            SigningCredentials SigningCredentials = GetSigningCredentials();

            List<Claim> UserClaims = GetClaims(AppUser);

            JwtSecurityToken TokenOptions = GetTokenOptions(SigningCredentials, UserClaims, AppUser.ExpirationJWTDateTime);

            string Token = new JwtSecurityTokenHandler().WriteToken(TokenOptions);

            return Task.FromResult(Token);
        }

        #region "GenerateJWTToken"
        private JwtSecurityToken GetTokenOptions(SigningCredentials signingCredentials, List<Claim> userClaims,DateTime Expires)
        {
            return new JwtSecurityToken(
                issuer: Configuration["JWTToken:ValidIssuer"],
                audience: Configuration["JWTToken:ValidAudience"],
                claims: userClaims,
                expires: Expires,
                signingCredentials: signingCredentials
                );
        }

        private List<Claim> GetClaims(AppUserDto AppUser)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, AppUser.UserID.ToString()),
                new Claim(ClaimTypes.Name, AppUser.UserName),
                new Claim(ClaimTypes.Role, AppUser.RoleCodes),
                new Claim(CustomClaimTypes.USER_FULLNAME, $"{AppUser.UserFirstName} {AppUser.UserLastName}"),
                new Claim(CustomClaimTypes.COMPANY_ID, AppUser.CompanyID.ToString()),
                new Claim(CustomClaimTypes.IDIOM_ID, AppUser.IdiomID.ToString()),
                new Claim(CustomClaimTypes.COMPANY_DOCUMENTNUMBER, AppUser.CompanyDocumentNumber),
                new Claim(CustomClaimTypes.COMPANY_SOCIALREASON, AppUser.CompanySocialReason),
                new Claim(CustomClaimTypes.COMPANY_TRADENAME, AppUser.CompanyTradeName)
            };

            if (AppUser.Permissions is not null)
            {
                foreach (var permission in AppUser.Permissions)
                {
                    Claims.Add(new Claim(CustomClaimTypes.PERMISSIONS, permission));
                }
            }
            return Claims;
        }

        private SigningCredentials GetSigningCredentials()
        {           
            return new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }
        #endregion "GenerateJWTToken"

        public Task<ClaimsPrincipalDto?> ValidateJWTToken(string JWTToken, bool IgnoreExpiration = false)
        {
            var Handler = new JwtSecurityTokenHandler(); 
            try{
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = !IgnoreExpiration,
                    IssuerSigningKey = SymmetricSecurityKey,
                    ValidIssuer = Configuration["JWTToken:ValidIssuer"],
                    ValidAudience = Configuration["JWTToken:ValidAudience"],
                    ClockSkew = TimeSpan.Zero // Opcional: sin margen extra de expiración
                };               
                var Principal = Handler.ValidateToken(JWTToken, parameters, out _);
                var UserID = Principal.FindFirst(ClaimTypes.Sid)?.Value;
                var UserName = Principal.FindFirst(ClaimTypes.Name)?.Value;
                var CompanyID = Principal.FindFirst(CustomClaimTypes.COMPANY_ID)?.Value;
                var Exp = Principal.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

                var ClaimsPrincipal = new ClaimsPrincipalDto();
                   ClaimsPrincipal.UserName = UserName ?? "";

                if (int.TryParse(UserID, out var ParseUserID))
                    ClaimsPrincipal.UserID = ParseUserID;

                if (int.TryParse(CompanyID, out var ParseCompanyID))
                    ClaimsPrincipal.CompanyID = ParseCompanyID;

                if (long.TryParse(Exp, out var expUnix))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                    ClaimsPrincipal.IsExpired = expDate <= DateTime.UtcNow;
                }

                return Task.FromResult<ClaimsPrincipalDto?>(ClaimsPrincipal);
            }
            catch(Exception ex){
                return Task.FromResult<ClaimsPrincipalDto?>(null);
            }
        }

        public Task<string> GenerateRandomToken(int ByteLength = 32)
        { 
            var Bytes = new byte[ByteLength];
            using (var RNG = RandomNumberGenerator.Create()){
                RNG.GetBytes(Bytes); 
            }
            var ToBase64String = Convert.ToBase64String(Bytes)
                                .Replace("+", "-")
                                .Replace("/", "_")
                                .Replace("=", "");

            return Task.FromResult<string>(ToBase64String);
        } 
 
    }
}
