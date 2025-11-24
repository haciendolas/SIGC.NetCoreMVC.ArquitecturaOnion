using MediatR;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageCompanyFeatures.Commands.PageCompanyCreate
{
    internal class PageCompanyDeleteCreateCommandHandler : IRequestHandler<PageCompanyDeleteCreateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IPageCompanyCreateRepository PageCompanyCreateRepository;
        private readonly IPageCompanyDeleteRepository PageCompanyDeleteRepository;      
        public PageCompanyDeleteCreateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IUnitOfWork UnitOfWork,
            IPageCompanyCreateRepository PageCompanyCreateRepository,
            IPageCompanyDeleteRepository PageCompanyDeleteRepository
        )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.UnitOfWork = UnitOfWork;
            this.PageCompanyCreateRepository = PageCompanyCreateRepository; 
            this.PageCompanyDeleteRepository = PageCompanyDeleteRepository;
        }

        public async Task<MsgResponse<object?>> Handle(PageCompanyDeleteCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                await PageCompanyDeleteRepository.DeleteAsync(Request.CompanyID);
                foreach (var PageID in Request.PageIDS)
                {
                    var PageCompany = new PageCompany()
                    {
                        PageID = PageID,
                        CompanyID = Request.CompanyID,
                        CreatedDateTime = DateTime.Now,
                        CreatedBy = CurrentSessionService.UserID
                    };
                    await PageCompanyCreateRepository.CreateAsync(PageCompany);
                }

                MsgResponse.Type = MessageTypeConst.SUCCESS;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);

                await UnitOfWork.CommitTransactionAsync(CancellationToken);
            }
            catch (Exception ex) {

                await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
       }
    }
}