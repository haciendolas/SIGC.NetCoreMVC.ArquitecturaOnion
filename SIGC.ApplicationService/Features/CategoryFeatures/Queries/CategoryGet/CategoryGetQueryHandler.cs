using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers; 

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryGet
{
    internal class CategoryGetQueryHandler(
      ICategoryGetRepository CategoryGetRepository,
      IMessageService MessageService,
      ICurrentSessionService CurrentSessionService,
      IFileStorageService FileStorageService,
      FileUploadSettings FileUploadSettings
    ) : IRequestHandler<CategoryGetQueryRequest, MsgResponse<CategoryGetResponseDto?>>
    {
        public async Task<MsgResponse<CategoryGetResponseDto?>> Handle(CategoryGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse =new MsgResponse<CategoryGetResponseDto?>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            var CategoryGet = await CategoryGetRepository.GetAsync(CurrentSessionService.CompanyID,Request.CategoryId, CancellationToken);
            if (CategoryGet is null)
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            else
            {
                FileEntryDto FileEntry = new FileEntryDto(CategoryGet.Value.CategoryImage, $"{FileUploadSettings.CategoryImageLocation}/{CategoryGet.Value.CategoryImage}");
                MsgResponse.Data = new CategoryGetResponseDto {
                    CategoryId = CategoryGet.Value.CategoryId,
                    CategoryName = CategoryGet.Value.CategoryName,
                    CategorySlug = CategoryGet.Value.CategorySlug,
                    CategoryImage = CategoryGet.Value.CategoryImage,
                    RecordStateID = CategoryGet.Value.RecordStateID,
                    CategoryUrl = string.IsNullOrWhiteSpace(CategoryGet.Value.CategoryImage) ? "" : FileStorageService.GetFileUrl(FileEntry)
                };                
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            }
            return MsgResponse;
        }
    }
}