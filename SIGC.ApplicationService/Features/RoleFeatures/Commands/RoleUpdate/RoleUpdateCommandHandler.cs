using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleUpdate
{
    internal class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IRoleUpdateRepository RoleUpdateRepository;
        private readonly IRoleVerifyCodeAndNameRepository RoleVerifyCodeAndNameRepository;
        private readonly IRolePermissionCreateRepository RolePermissionCreateRepository;
        private readonly IRolePermissionDeleteRepository RolePermissionDeleteRepository;
        public RoleUpdateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IRoleUpdateRepository RoleUpdateRepository,
            IRoleVerifyCodeAndNameRepository RoleVerifyCodeAndNameRepository,
            IRolePermissionCreateRepository RolePermissionCreateRepository,
            IRolePermissionDeleteRepository RolePermissionDeleteRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.RoleUpdateRepository = RoleUpdateRepository;
            this.RoleVerifyCodeAndNameRepository = RoleVerifyCodeAndNameRepository;
            this.RolePermissionCreateRepository = RolePermissionCreateRepository;
            this.RolePermissionDeleteRepository = RolePermissionDeleteRepository;
        }

        public async Task<MsgResponse<object?>> Handle(RoleUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Role.Update(
                        Request.RoleID,
                        Request.CompanyID,
                        Request.RoleCode,
                        Request.RoleName,
                        Request.RoleDescription,
                        Request.StateID,
                        DateTime.Now,
                       CurrentSessionService.UserID
                    );

                var Verify = await RoleVerifyCodeAndNameRepository.VerifyCodeAndNameAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Role.OK)
                {
                    int RecordAffected = await RoleUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        await RolePermissionDeleteRepository.DeleteAsync(Request.RoleID, CancellationToken);
                        foreach (var Item in Request.RolePermission)
                        {
                            await RolePermissionCreateRepository.CreateAsync(new RolePermission
                            {
                                CompanyID = Request.CompanyID,
                                RoleID = Model.RoleID,
                                PageID = Item.PageID,
                                PageActionID = Item.PageActionID,
                                PageRoleCreatedDateTime = Model.CreatedDateTime
                            });
                        }
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_UPDATE);
                        MsgResponse.Data = new
                        {
                            Model.RoleID,
                            Model.RoleCode,
                            Model.RoleName,
                            Model.CreatedDateTime,
                        };
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_UPDATE);
                    }
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(Verify == VerifyRegistryConst.Role.NAME_EXISTS ? MessageDescriptionConst.EXIST_ROLE_ROLENAME : MessageDescriptionConst.EXIST_ROLE_ROLECODE);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = "El codigo de rol es obligatorio";
            }
            catch (Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }    
    }
}