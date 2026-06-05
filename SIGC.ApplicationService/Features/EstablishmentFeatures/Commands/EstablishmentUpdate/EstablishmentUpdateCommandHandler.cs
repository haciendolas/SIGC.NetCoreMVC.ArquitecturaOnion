using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentUpdate
{
    internal class EstablishmentUpdateCommandHandler : IRequestHandler<EstablishmentUpdateCommandRequest, MsgResponse<object?>>
    {
        private readonly FileUploadSettings FileUploadSettings;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IFileStorageService FileStorageService;
        private readonly IEstablishmentUpdateRepository EstablishmentUpdateRepository;

        public EstablishmentUpdateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IEstablishmentUpdateRepository EstablishmentUpdateRepository
            )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;
            this.EstablishmentUpdateRepository = EstablishmentUpdateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(EstablishmentUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Establishment.Update(
                        CurrentSessionService.CompanyID,
                        Request.EstablishmentID,
                        Request.PersonID.HasValue ? Request.PersonID.Value : CurrentSessionService.CompanyID,
                        Request.TypeID,
                        Request.EstablishmentCode,
                        Request.EstablishmentName,
                        Request.EstablishmentAddress,                    
                        Request.EstablishmentLogoBandera == "DELETE" ? null : Request.File == null ? Request.EstablishmentLogo : $"{CurrentDate.ToString("ddMMyyyyHHmm")}_{Request.EstablishmentName}{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordStateId,
                        CurrentDate,
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );  

                var Validate = await EstablishmentUpdateRepository.UpdateAsync(Model,CancellationToken);
                if (Validate == VerifyRegistryConst.Category.OK)
                { 
                     if (Request.File is not null){
                         if (!string.IsNullOrWhiteSpace(Request.EstablishmentLogo)){
                            FileEntry.FileName = Request.EstablishmentLogo;
                            FileEntry.FileLocation = $"{FileUploadSettings.EstablishmentLogoLocation}/{Request.EstablishmentLogo}";
                            await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                         }
                         FileEntry.FileName = Model.EstablishmentLogo;
                         FileEntry.FileLocation = $"{FileUploadSettings.EstablishmentLogoLocation}/{Model.EstablishmentLogo}";
                         await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                     }

                     if (Request.EstablishmentLogoBandera == "DELETE"){
                            FileEntry.FileName = Request.EstablishmentLogo;
                            FileEntry.FileLocation = $"{FileUploadSettings.EstablishmentLogoLocation}/{Request.EstablishmentLogo}";
                            await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                     }
                     MsgResponse.Type = MessageTypeConst.SUCCESS;
                     MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_UPDATE);
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_ESTABLISHMENT_ESTABLISHMENTNAME);
                }
            }
            catch(Exception ex)
            {
                if (Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);

                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}