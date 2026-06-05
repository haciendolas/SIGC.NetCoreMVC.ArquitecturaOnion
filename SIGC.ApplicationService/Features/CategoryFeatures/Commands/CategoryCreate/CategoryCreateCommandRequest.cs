using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryCreate
{
    public class CategoryCreateCommandRequest : IRequest<MsgResponse<object>>
    {
        public string CategoryName { get; set; } = null!;
        public string CategorySlug { get; set; } = null!;
        public RecordOriginEnum RecordOriginId { get; set; }
        public RecordStateEnum RecordStateId { get; set; }
        public IFileDataService? File { get; set; }
    }
}
