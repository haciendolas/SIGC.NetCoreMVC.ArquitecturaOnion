using SIGC.Presentation.AspNetCoreMVC.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace SIGC.Presentation.AspNetCoreMVC.Helpers
{
    public static class ConvertsHelper
    {
        /// <summary>
        /// Separa las propiedades normales y las propiedades IFormFile de un objeto.
        /// Devuelve un nuevo objeto sin las propiedades IFormFile, y un diccionario con los archivos.
        /// </summary>
        /// <typeparam name="TDto">Tipo del DTO original</typeparam>
        /// <param name="obj">Instancia del DTO con propiedades normales y archivos</param>
        /// <returns>
        /// Tupla con:
        /// - Nuevo objeto sin propiedades IFormFile
        /// - Diccionario con nombre de propiedad y el Stream, nombre y tipo de contenido del archivo
        /// </returns>
        public static (TDto dtoSinArchivos, Dictionary<string, (Stream Stream, string FileName, string ContentType)> archivos)
            ExtraerArchivosYDto<TDto>(TDto obj)
            where TDto : new()
        {
            var dtoSinArchivos = new TDto();
            var archivos = new Dictionary<string, (Stream, string, string)>();

            var props = typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                if (value == null)
                    continue;

                if (typeof(IFormFile).IsAssignableFrom(prop.PropertyType))
                {
                    var formFile = (IFormFile)value;
                    archivos.Add(prop.Name, (formFile.OpenReadStream(), formFile.FileName, formFile.ContentType));
                }
                else
                {
                    prop.SetValue(dtoSinArchivos, value);
                }
            }
            return (dtoSinArchivos, archivos);
        }

        public static AuthenticationIdentity ExtractUserInfo(string JwtToken)
        {
            var Handler = new JwtSecurityTokenHandler();

            var JwtSecurityToken = Handler.ReadJwtToken(JwtToken);

            var AuthenticationIdentity = new AuthenticationIdentity();

            if (int.TryParse(JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value, out var ParseUserID))
                AuthenticationIdentity.UserID = ParseUserID;

            if (int.TryParse(JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ConstantsHelper.CustomClaimTypes.COMPANY_ID)?.Value, out var ParseCompanyID))
                AuthenticationIdentity.CompanyID = ParseCompanyID;

            AuthenticationIdentity.CompanyDocumentNumber = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ConstantsHelper.CustomClaimTypes.COMPANY_DOCUMENTNUMBER)?.Value ?? "";
            AuthenticationIdentity.UserName = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
            AuthenticationIdentity.CompanySocialReason = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ConstantsHelper.CustomClaimTypes.COMPANY_SOCIALREASON)?.Value ?? "";
            AuthenticationIdentity.CompanyTradeName = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ConstantsHelper.CustomClaimTypes.COMPANY_TRADENAME)?.Value ?? "";

            AuthenticationIdentity.UserFirstName = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ConstantsHelper.CustomClaimTypes.COMPANY_DOCUMENTNUMBER)?.Value ?? "";

            var RoleCodes = JwtSecurityToken.Claims.First(f => f.Type == ClaimTypes.Role).Value;
            var RoleList = RoleCodes.Contains(',') ? RoleCodes.Split(",").ToList() : new List<string> { RoleCodes };

            AuthenticationIdentity.RoleList = RoleList;

            return AuthenticationIdentity;
        }

        public static Dictionary<string, string> GetQueryParams(object obj)
        {
            var dict = new Dictionary<string, string>();
            if (obj == null) return dict;
            foreach (var prop in obj.GetType().GetProperties())
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    dict[prop.Name] = value.ToString();
                }
            }
            return dict;
        }
    }
}