using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryUpdate
{
    public class CategoryUpdateCommandRequest : IRequest<MsgResponse<object?>>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategorySlug { get; set; } = null!; 
        public RecordStateEnum RecordStateId { get; set; }
        public IFileDataService? File { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryImageBandera { get; set; }
    }
}