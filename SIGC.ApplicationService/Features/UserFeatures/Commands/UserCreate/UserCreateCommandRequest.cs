using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserFeatures.Commands.UserCreate
{
    public class UserCreateCommandRequest:IRequest<MsgResponse<object?>>
    {
        public int CompanyID { get; set; }
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string? UserMail { get; set; }
        public RecordStateEnum StateID { get; set; }
        public IFileDataService? File { get; set; }
        public List<int> RoleIDs { get; set; } = new List<int>();
    }
}