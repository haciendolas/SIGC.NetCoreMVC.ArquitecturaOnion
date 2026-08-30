using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentCreate
{
    internal class EstablishmentCreateCommandHandler : IRequestHandler<EstablishmentCreateCommandRequest, MsgResponse<object>>
    {
        private readonly FileUploadSettings FileUploadSettings;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;   
        private readonly IFileStorageService FileStorageService;  
        private readonly IEstablishmentCreateRepository EstablishmentCreateRepository;

        public EstablishmentCreateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IEstablishmentCreateRepository EstablishmentCreateRepository
            )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;         
            this.EstablishmentCreateRepository = EstablishmentCreateRepository;
        }
        public async Task<MsgResponse<object>> Handle(EstablishmentCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Establishment.Create(
                        CurrentSessionService.CompanyID,
                        Request.PersonID.HasValue ? Request.PersonID.Value :  CurrentSessionService.CompanyID,
                        Request.TypeID,
                        Request.EstablishmentCode,
                        Request.EstablishmentName,
                        Request.EstablishmentAddress,
                        Request.File == null ? null : $"{CurrentDate.ToString("ddMMyyyyHHmm")}_{Request.EstablishmentName}{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordOriginId,
                        Request.RecordStateId,
                        CurrentDate,
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );

                var Verify = await EstablishmentCreateRepository.CreateAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Establishment.OK)
                {                    
                    if (Model.EstablishmentID > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.EstablishmentLogo;
                            FileEntry.FileLocation = $"{FileUploadSettings.EstablishmentLogoLocation}/{Model.EstablishmentLogo}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.EstablishmentID,
                            Model.EstablishmentCode,
                            Model.EstablishmentName,
                            Model.RecordStateId,
                            Model.CreatedDate,
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
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_ESTABLISHMENT_ESTABLISHMENTNAME);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = ae.Message;
            }
            catch (Exception ex)
            {
                if (Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);

                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }
    }
}