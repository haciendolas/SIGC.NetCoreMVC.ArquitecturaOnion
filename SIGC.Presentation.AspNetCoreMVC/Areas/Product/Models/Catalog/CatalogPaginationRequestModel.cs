using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog
{
    public class CatalogPaginationRequestModel: DataTableHelper
    {
        public byte? CatalogTypeID { get; set; }
        public byte? CategoryID { get; set; }
        public byte? ManufacturerID { get; set; }
        public byte? BrandID { get; set; }
        public byte RecordStateID { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
