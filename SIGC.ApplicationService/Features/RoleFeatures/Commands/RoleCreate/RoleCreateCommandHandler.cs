using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleCreate
{
    internal class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IRoleCreateRepository RoleCreateRepository;
        private readonly IRoleVerifyCodeAndNameRepository RoleVerifyCodeAndNameRepository;
        private readonly IRolePermissionCreateRepository RolePermissionCreateRepository;

        public RoleCreateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IRoleCreateRepository RoleCreateRepository,
            IRoleVerifyCodeAndNameRepository RoleVerifyCodeAndNameRepository,
            IRolePermissionCreateRepository RolePermissionCreateRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.RoleCreateRepository = RoleCreateRepository;
            this.RoleVerifyCodeAndNameRepository = RoleVerifyCodeAndNameRepository;
            this.RolePermissionCreateRepository = RolePermissionCreateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(RoleCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Role.Create(
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
                    int RecordAffected = await RoleCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        foreach (var Item in Request.RolePermission)
                        {
                            await RolePermissionCreateRepository.CreateAsync(new RolePermission
                            {
                                CompanyID = Item.CompanyID,
                                RoleID = Model.RoleID,
                                PageID = Item.PageID,
                                PageActionID = Item.PageActionID,
                                PageRoleCreatedDateTime = Model.CreatedDateTime
                            });
                        }
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_INSERT);
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
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }                
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message =MessageService.GetMessageResult(Verify == VerifyRegistryConst.Role.NAME_EXISTS ?  MessageDescriptionConst.EXIST_ROLE_ROLENAME: MessageDescriptionConst.EXIST_ROLE_ROLECODE);
                }
            }
            catch(ArgumentNullException ae)
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
