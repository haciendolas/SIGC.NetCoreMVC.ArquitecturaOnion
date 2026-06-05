using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserCompanyFeatures.Queries.UserCompanyGet
{
    internal class UserCompanyGetQueryHandler : IRequestHandler<UserCompanyGetQueryRequest, MsgResponse<UserCompanyGetQueryResponse?>>
    {
        private readonly IMessageService MessageService;
        private readonly IFileStorageService FileStorageService;    
        private readonly IUserCompanyGetRepository UserCompanyGetRepository;        
        private readonly FileUploadSettings FileUploadSettings;

        public UserCompanyGetQueryHandler(
                 FileUploadSettings FileUploadSettings, 
                 IMessageService MessageService, 
                 IFileStorageService FileStorageService, 
                 IUserCompanyGetRepository UserCompanyGetRepository)
        {
            this.FileUploadSettings = FileUploadSettings;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;
            this.UserCompanyGetRepository = UserCompanyGetRepository;      
        }

        public async Task<MsgResponse<UserCompanyGetQueryResponse?>> Handle(UserCompanyGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<UserCompanyGetQueryResponse?>();
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            var UserCompanyGet = await UserCompanyGetRepository.GetAsync(Request.UserID,Request.CompanyID, CancellationToken);

            if (UserCompanyGet is not null)
            { 
                MsgResponse.Type = MessageTypeConst.QUERY;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);

                FileEntryDto FileEntry = new FileEntryDto(UserCompanyGet.Value.UserPhoto, $"{FileUploadSettings.UserPhotoLocation}/{UserCompanyGet.Value.UserPhoto}");

                var UserCompanyResponse = new UserCompanyGetQueryResponse()
                {
                    UserID = UserCompanyGet.Value.UserID,
                    UserFirstName=UserCompanyGet.Value.UserFirstName,
                    UserLastName=UserCompanyGet.Value.UserLastName,
                    UserName = UserCompanyGet.Value.UserName,
                    UserPassword=UserCompanyGet.Value.UserPassword,
                    UserMail=UserCompanyGet.Value.UserMail,
                    UserPhoto=UserCompanyGet.Value.UserPhoto,
                    UserUrl = string.IsNullOrWhiteSpace(UserCompanyGet.Value.UserPhoto) ? "" : FileStorageService.GetFileUrl(FileEntry),
                    StateID = UserCompanyGet.Value.StateID,
                    RoleIDs = UserCompanyGet.Value.RoleIDs,
                };  

                MsgResponse.Data = UserCompanyResponse;
            }
            return MsgResponse;
        }
    }
}
