using Microsoft.AspNetCore.Http;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using System.Security.Claims;

namespace SIGC.Infrastructure.GeneralService.Services
{
    internal class CurrentSessionService : ICurrentSessionService
    {
        readonly IHttpContextAccessor Context;

        public CurrentSessionService(IHttpContextAccessor context)
        {
            Context = context;
        }
        public bool IsAuthenticated =>
           Context.HttpContext.User.Identity!.IsAuthenticated;
        public string UserName =>
            Context.HttpContext.User.Identity?.Name!; //Context.HttpContext.User!.Identity!.Name ?? "Unknown";

        public int UserID =>
            Convert.ToInt32(Context.HttpContext.User?
            .FindFirst(ClaimTypes.Sid).Value);

        public int CompanyID =>
             Convert.ToInt32(Context.HttpContext.User.Claims
            .First(q => q.Type == CustomClaimTypes.COMPANY_ID).Value);

        public string CompanyDocumentNumber =>
            Context.HttpContext.User.Claims
            .First(q => q.Type == CustomClaimTypes.COMPANY_DOCUMENTNUMBER).Value;

        public string CompanySocialReason =>
            Context.HttpContext.User.Claims
            .First(f => f.Type == CustomClaimTypes.COMPANY_SOCIALREASON).Value;

        public string CompanyTradeName =>
             Context.HttpContext.User.Claims
            .First(f => f.Type == CustomClaimTypes.COMPANY_TRADENAME).Value;

        public List<string> RoleList => Roles();

        public short IdiomID =>
             Convert.ToInt16(Context.HttpContext.User.Claims
            .First(q => q.Type == CustomClaimTypes.IDIOM_ID).Value);

        private List<string> Roles()
        {
            var RoleCodes = Context.HttpContext.User.Claims
               .First(f => f.Type == ClaimTypes.Role)
               .Value;

            var RoleList = RoleCodes.Contains(',')
                ? RoleCodes.Split(",").ToList()
                : new List<string> { RoleCodes };

            return RoleList;
        }
    }
}
