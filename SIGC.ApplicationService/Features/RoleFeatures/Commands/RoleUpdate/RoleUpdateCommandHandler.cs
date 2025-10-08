using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
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
        private readonly IPageCompanyCreateNotExistsRepository PageCompanyCreateNotExistsRepository;
        private readonly IUnitOfWork UnitOfWork;
        public RoleUpdateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IRoleUpdateRepository RoleUpdateRepository,
            IRoleVerifyCodeAndNameRepository RoleVerifyCodeAndNameRepository,
            IRolePermissionCreateRepository RolePermissionCreateRepository,
            IRolePermissionDeleteRepository RolePermissionDeleteRepository,
            IPageCompanyCreateNotExistsRepository PageCompanyCreateNotExistsRepository,
            IUnitOfWork UnitOfWork
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.RoleUpdateRepository = RoleUpdateRepository;
            this.RoleVerifyCodeAndNameRepository = RoleVerifyCodeAndNameRepository;
            this.RolePermissionCreateRepository = RolePermissionCreateRepository;
            this.RolePermissionDeleteRepository = RolePermissionDeleteRepository;
            this.PageCompanyCreateNotExistsRepository = PageCompanyCreateNotExistsRepository;
            this.UnitOfWork = UnitOfWork;
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
                    await UnitOfWork.BeginTransactionAsync(CancellationToken);
                    int RecordAffected = await RoleUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        await RolePermissionDeleteRepository.DeleteAsync(Request.RoleID, CancellationToken);

                        var PageIDs = Request.RolePermission.Select(s => s.PageID).Distinct().ToList();
                        foreach (var PageID in PageIDs)
                        {
                            var PageCompany = new PageCompany()
                            {
                                PageID = PageID,
                                CompanyID = Request.CompanyID,
                                CreatedDateTime = Model.CreatedDateTime,
                                CreatedBy = CurrentSessionService.UserID
                            };
                            await PageCompanyCreateNotExistsRepository.CreateNotExistsAsync(PageCompany, CancellationToken);

                            foreach (var Item in Request.RolePermission.Where(w => w.PageID == PageID).ToList())
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
              
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);
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
                await UnitOfWork.RollbackTransactionAsync(CancellationToken);
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }    
    }
}