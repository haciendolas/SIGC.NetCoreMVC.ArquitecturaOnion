using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Enums;
using SIGC.DomainModel.Models;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.IRepositories.IUserRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserFeatures.Commands.UserCreate
{
    internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IFileStorageService FileStorageService;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IUserVerifyNameAndMailRepository UserVerifyNameAndMailRepository;
        private readonly IUserCreateRepository UserCreateRepository;
        private readonly IUserCompanyCreateRepository UserCompanyCreateRepository;
        private readonly IUserRoleCreateRepository UserRoleCreateRepository;
        private readonly FileUploadSettings FileUploadSettings;

        public UserCreateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IUnitOfWork UnitOfWork,
            IUserVerifyNameAndMailRepository UserVerifyNameAndMailRepository,
            IUserCreateRepository UserCreateRepository,
            IUserCompanyCreateRepository UserCompanyCreateRepository,
            IUserRoleCreateRepository UserRoleCreateRepository           
        )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;
            this.UnitOfWork = UnitOfWork;
            this.UserVerifyNameAndMailRepository = UserVerifyNameAndMailRepository;
            this.UserCreateRepository = UserCreateRepository;
            this.UserCompanyCreateRepository = UserCompanyCreateRepository;
            this.UserRoleCreateRepository = UserRoleCreateRepository;
        }

        public async  Task<MsgResponse<object?>> Handle(UserCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try {
                var Model = User.Create( Request.UserFirstName, 
                                         Request.UserLastName,
                                         Request.UserName,
                                         Request.UserPassword,
                                         Request.UserMail,
                                         Request.File == null ? null : $"{Request.UserName.Replace(" ","").ToUpper().Trim()}{Path.GetExtension(Request.File.FileName)}",
                                         RecordStateEnum.Active,
                                         DateTime.Now,
                                         CurrentSessionService.UserID
                                     );
                var Verify = await UserVerifyNameAndMailRepository.VerifyNameAndMailAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.User.OK)
                {
                    await UnitOfWork.BeginTransactionAsync(CancellationToken);

                    int RecordAffected = await UserCreateRepository.CreateAsync(Model, CancellationToken);
                        RecordAffected = await UserCompanyCreateRepository.CreateAsync(new UserCompany
                                                                                {
                                                                                 CompanyID = Request.CompanyID,
                                                                                 UserID = Model.UserId,
                                                                                 StateID = Request.StateID,
                                                                                 CreatedBy = Model.CreatedBy,
                                                                                 CreatedDateTime = Model.CreatedDateTime
                                                                                });
                   
                    foreach (var RoleID in Request.RoleIDs)
                    {
                        RecordAffected = await UserRoleCreateRepository.CreateAsync(new UserRole { 
                                                         CompanyID = Request.CompanyID,
                                                         RoleID = RoleID, 
                                                         UserID = Model.UserId                         
                                                         });
                    }

                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.UserPhoto;
                            FileEntry.FileLocation = $"{FileUploadSettings.UserPhotoLocation}/{Model.UserPhoto}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_INSERT);

                        await UnitOfWork.CommitTransactionAsync(CancellationToken);
                    }
                    else{

                        await UnitOfWork.RollbackTransactionAsync(CancellationToken);
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else if(Verify == VerifyRegistryConst.User.USER_EXISTS)
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_USER_USERNAME);
                }
                else if (Verify == VerifyRegistryConst.User.MAIL_EXISTS)
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_USER_USERMAIL);
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_USER_NAME_AND_MAIL);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = ae.Message;
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                if (Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}