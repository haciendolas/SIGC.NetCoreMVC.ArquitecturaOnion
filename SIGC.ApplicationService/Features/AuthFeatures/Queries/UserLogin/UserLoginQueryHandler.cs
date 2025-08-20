using MediatR;
using SIGC.ApplicationService.Commons;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.UserLogin
{
    internal class UserLoginQueryHandler : IRequestHandler<UserLoginQueryRequest, string>
    {
        public async Task<string> Handle(UserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            var response = UserList().FirstOrDefault(f => f.UserPassword == request.UserPassword && f.UserName == request.UserName && f.CompanyDocumentNumber == request.CompanyDocumentNumber);
            if (response is null) return "Usuario y clave incorrecto";
            return "Bienvenido al sistema";
        }

        private List<UserResponseDto> UserList()
        {
            var list = new List<UserResponseDto>() {
               new UserResponseDto
               {
                   UserId=1,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358087",
                   UserName="Administrador",
                   UserPassword="123456"
               },
               new UserResponseDto
               {
                   UserId=2,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358086",
                   UserName="jcastillo",
                   UserPassword="123456"
               }
            };

            return list;
        }

    }
}
