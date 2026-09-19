using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogActiveIngredientRepositories;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IRepositories.ICatalogTherapeuticActionRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogUpdate
{
    internal class CatalogUpdateCommandHandler: IRequestHandler<CatalogUpdateCommandRequest, MsgResponse<object?>>
     {
        private readonly ICatalogUpdateRepository CatalogUpdateRepository;
        private readonly ICatalogVerifyCodeAndNameRepository CatalogVerifyCodeAndNameRepository;
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IFileStorageService FileStorageService;
        private readonly FileUploadSettings FileUploadSettings;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICatalogActiveIngredientCreateUpdateRepository CatalogActiveIngredientCreateUpdateRepository;
        private readonly ICatalogTherapeuticActionCreateUpdateRepository CatalogTherapeuticActionCreateUpdateRepository;
        private readonly ICatalogActiveIngredientDeleteRepository CatalogActiveIngredientDeleteRepository;
        private readonly ICatalogTherapeuticActionDeleteRepository CatalogTherapeuticActionDeleteRepository;
        public CatalogUpdateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IUnitOfWork UnitOfWork,
            ICatalogVerifyCodeAndNameRepository CatalogVerifyCodeAndNameRepository,
            ICatalogUpdateRepository CatalogUpdateRepository,
            ICatalogActiveIngredientCreateUpdateRepository CatalogActiveIngredientCreateUpdateRepository,
            ICatalogTherapeuticActionCreateUpdateRepository CatalogTherapeuticActionCreateUpdateRepository,
            ICatalogActiveIngredientDeleteRepository CatalogActiveIngredientDeleteRepository,
            ICatalogTherapeuticActionDeleteRepository CatalogTherapeuticActionDeleteRepository
        )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.FileStorageService = FileStorageService;
            this.UnitOfWork = UnitOfWork;
            this.CatalogVerifyCodeAndNameRepository = CatalogVerifyCodeAndNameRepository;
            this.CatalogUpdateRepository = CatalogUpdateRepository;
            this.CatalogActiveIngredientCreateUpdateRepository = CatalogActiveIngredientCreateUpdateRepository;
            this.CatalogTherapeuticActionCreateUpdateRepository = CatalogTherapeuticActionCreateUpdateRepository;
            this.CatalogActiveIngredientDeleteRepository = CatalogActiveIngredientDeleteRepository;
            this.CatalogTherapeuticActionDeleteRepository = CatalogTherapeuticActionDeleteRepository;
        }
        public async Task<MsgResponse<object?>> Handle(CatalogUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Catalog.Update(
                      CurrentSessionService.CompanyID,
                      Request.CatalogID,
                        Request.CatalogTypeID,
                        Request.CategoryID,
                        Request.CatalogCode,
                        Request.CatalogSlug,
                        Request.CatalogName,
                        Request.CatalogHasVariants,
                        Request.SaleConditionID,
                        Request.ManufacturerID,
                        Request.BrandID,
                        Request.PharmaceuticalFormID,
                        Request.CatalogBrandType,
                        Request.CatalogConcentration,
                        Request.CatalogDescription,
                        Request.CatalogImageBandera == "DELETE" ? null : Request.File == null ? Request.CatalogImage : $"{CurrentDate.ToString("ddMMyyyyHHmmss")}{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordOriginID,
                        Request.RecordStateID,
                        CurrentDate,
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserName
                    );
                foreach (var CatalogActiveIngredient in Request.CatalogActiveIngredients)
                {
                    Model.AddCatalogActiveIngredient(
                                            CatalogActiveIngredient.ActiveIngredientID,
                                            CatalogActiveIngredient.CatalogActiveIngredientQuantity,
                                            CatalogActiveIngredient.UnitMeasureID,
                                            CatalogActiveIngredient.CatalogActiveIngredientLabel
                                            );
                }
                foreach (var CatalogTherapeuticAction in Request.CatalogTherapeuticActions)
                {
                    Model.AddCatalogTherapeuticAction(CatalogTherapeuticAction.TherapeuticActionID);
                }

                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogVerifyCodeAndNameRepository.VerifyCodeAndNameAsync(Model,CancellationToken);
                if (Verify == VerifyRegistryConst.Catalog.OK)
                {
                    var RecordAffected = await CatalogUpdateRepository.UpdateAsync(Model, CancellationToken);
                    
                    await CatalogActiveIngredientDeleteRepository.DeleteAsync(Model.CompanyID, Model.CatalogID, Request.CatalogActiveIngredients.Select(s => s.ActiveIngredientID).ToList(),CancellationToken);
                    await CatalogTherapeuticActionDeleteRepository.DeleteAsync(Model.CompanyID, Model.CatalogID, Request.CatalogTherapeuticActions.Select(s => s.TherapeuticActionID).ToList(),CancellationToken);
                    
                    foreach (var CatalogActiveIngredient in Model.CatalogActiveIngredients)
                    {
                        await CatalogActiveIngredientCreateUpdateRepository.CreateUpdateAsync(CatalogActiveIngredient, CancellationToken);
                    }
                    foreach (var CatalogTherapeuticAction in Model.CatalogTherapeuticActions)
                    {
                        await CatalogTherapeuticActionCreateUpdateRepository.CreateUpdateAsync(CatalogTherapeuticAction, CancellationToken);
                    }
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            if (!string.IsNullOrWhiteSpace(Request.CatalogImage))
                            {
                                FileEntry.FileName = Request.CatalogImage;
                                FileEntry.FileLocation = $"{FileUploadSettings.CatalogImageLocation}/{Request.CatalogImage}";
                                await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                            }

                            FileEntry.FileName = Model.CatalogImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CatalogImageLocation}/{Model.CatalogImage}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        if (Request.CatalogImageBandera == "DELETE")
                        {
                            FileEntry.FileName = Request.CatalogImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CatalogImageLocation}/{Request.CatalogImage}";
                            await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                        }

                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_UPDATE);
                    }
                    else{
                        await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_UPDATE); 
                    }
                }
                else{
                    await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                    if (Verify == VerifyRegistryConst.Catalog.CODE_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOG_CATALOGCODE);
                    }
                    else if (Verify == VerifyRegistryConst.Catalog.NAME_EXISTS)
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOG_CATALOGNAME);
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.WARNING;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATALOG_CODE_AND_NAME);
                    }
                }
            }
            catch(Exception ex)
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