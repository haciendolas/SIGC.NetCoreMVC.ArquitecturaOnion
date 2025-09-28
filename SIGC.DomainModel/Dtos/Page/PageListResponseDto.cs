using SIGC.DomainModel.Dtos.Commons;

namespace SIGC.DomainModel.Dtos.Page
{    public record struct PageListResponseDto
    (
        int PageID,
        int PageParentID,
        string PageHierarchy,
        string PageName,
        string PageIconName,
        short PageOrder,
        List<PageActionResponseDto> PageAction
     ); 
}