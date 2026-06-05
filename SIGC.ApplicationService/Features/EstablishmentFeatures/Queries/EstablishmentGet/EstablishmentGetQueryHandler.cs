using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentGet
{
    internal class EstablishmentGetQueryHandler : IRequestHandler<EstablishmentGetQueryRequest, MsgResponse<EstablishmentGetResponseDto?>>
    {
        private readonly FileUploadSettings FileUploadSettings;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IFileStorageService FileStorageService;
        private readonly IEstablishmentGetRepository EstablishmentGetRepository;

        public EstablishmentGetQueryHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IEstablishmentGetRepository EstablishmentGetRepository
        )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;         
            this.EstablishmentGetRepository = EstablishmentGetRepository;
        }
        public async Task<MsgResponse<EstablishmentGetResponseDto?>> Handle(EstablishmentGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<EstablishmentGetResponseDto?>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            var EstablishmentGet = await EstablishmentGetRepository.GetAsync(CurrentSessionService.CompanyID, Request.EstablishmentID, CancellationToken);
            if (EstablishmentGet is null)
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            else
            {
                FileEntryDto FileEntry = new FileEntryDto(EstablishmentGet.EstablishmentLogo, $"{FileUploadSettings.EstablishmentLogoLocation}/{EstablishmentGet.EstablishmentLogo}");
                MsgResponse.Data = new EstablishmentGetResponseDto(
                
                    EstablishmentID : EstablishmentGet.EstablishmentID,
                    TypeID : EstablishmentGet.TypeID,
                    EstablishmentCode : EstablishmentGet.EstablishmentCode,
                    EstablishmentName : EstablishmentGet.EstablishmentName,
                    EstablishmentAddress : EstablishmentGet.EstablishmentAddress,
                    EstablishmentLogo : EstablishmentGet.EstablishmentLogo,
                    RecordStateID : EstablishmentGet.RecordStateID,
                    EstablishmentUrl : string.IsNullOrWhiteSpace(EstablishmentGet.EstablishmentLogo) ? "" : FileStorageService.GetFileUrl(FileEntry)
                );
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            }
            return MsgResponse;
        }
    }
}
