namespace SIGC.DomainModel.Dtos.PageCompany
{
    public record struct PageCompanyListResponseDto
    (
        int PageID,
        int PageParentID,
        string PageHierarchy,
        string PageName,
        string PageIconName,
        short PageOrder
    );
}