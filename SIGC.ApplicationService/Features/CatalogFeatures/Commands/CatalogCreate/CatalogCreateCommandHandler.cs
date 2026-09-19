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

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogCreate
{ 
    internal class CatalogCreateCommandHandler : IRequestHandler<CatalogCreateCommandRequest, MsgResponse<object>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IFileStorageService FileStorageService;
        private readonly FileUploadSettings FileUploadSettings;
        private readonly IUnitOfWork UnitOfWork;
        private readonly ICatalogCreateRepository CatalogCreateRepository;
        private readonly ICatalogVerifyCodeAndNameRepository CatalogVerifyCodeAndNameRepository;
        private readonly ICatalogActiveIngredientCreateUpdateRepository CatalogActiveIngredientCreateUpdateRepository;
        private readonly ICatalogTherapeuticActionCreateUpdateRepository CatalogTherapeuticActionCreateUpdateRepository;
        public CatalogCreateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IFileStorageService FileStorageService,
            IUnitOfWork UnitOfWork,
            ICatalogVerifyCodeAndNameRepository CatalogVerifyCodeAndNameRepository,
            ICatalogCreateRepository CatalogCreateRepository,
            ICatalogActiveIngredientCreateUpdateRepository CatalogActiveIngredientCreateUpdateRepository,
            ICatalogTherapeuticActionCreateUpdateRepository CatalogTherapeuticActionCreateUpdateRepository
        )
        {
            this.FileUploadSettings=FileUploadSettings;
            this.CurrentSessionService=CurrentSessionService;
            this.MessageService=MessageService;      
            this.FileStorageService=FileStorageService; 
            this.UnitOfWork=UnitOfWork;
            this.CatalogVerifyCodeAndNameRepository = CatalogVerifyCodeAndNameRepository;
            this.CatalogCreateRepository = CatalogCreateRepository;
            this.CatalogActiveIngredientCreateUpdateRepository = CatalogActiveIngredientCreateUpdateRepository;
            this.CatalogTherapeuticActionCreateUpdateRepository = CatalogTherapeuticActionCreateUpdateRepository;
        }

        public async Task<MsgResponse<object>> Handle(CatalogCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Catalog.Create(
                        CurrentSessionService.CompanyID,
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
                        Request.File == null ? null : $"{CurrentDate.ToString("ddMMyyyyHHmmss")}{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordOriginID,
                        Request.RecordStateID,                    
                        CurrentDate, 
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    ); 


                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CatalogVerifyCodeAndNameRepository.VerifyCodeAndNameAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Catalog.OK)
                {
                    int RecordAffected = await CatalogCreateRepository.CreateAsync(Model, CancellationToken);
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

                    foreach (var CatalogActiveIngredient in Model.CatalogActiveIngredients)
                    {
                        RecordAffected = await CatalogActiveIngredientCreateUpdateRepository.CreateUpdateAsync(CatalogActiveIngredient, CancellationToken);
                    }
                    foreach (var CatalogTherapeuticAction in Model.CatalogTherapeuticActions)
                    {
                        RecordAffected = await CatalogTherapeuticActionCreateUpdateRepository.CreateUpdateAsync(CatalogTherapeuticAction, CancellationToken);
                    }
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.CatalogImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CatalogImageLocation}/{Model.CatalogImage}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }
                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogID,
                            Model.CatalogName,
                            Model.RecordStateID,
                            Model.CreatedDate,
                        };                     
                    }
                    else
                    {
                        await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
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
