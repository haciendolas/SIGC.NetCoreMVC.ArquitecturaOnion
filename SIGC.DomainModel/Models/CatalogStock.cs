using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
    public class CatalogStock
    {
        public int CompanyID { get; set; }
        public int CatalogStockID { get; set; }
        public int CatalogVariantID { get; private set; }
        public int WarehouseID { get; private set; }
        public decimal? CatalogStockCurrentQuantity { get; private set; }
        public decimal CatalogStockMinimumQuantity { get; private set; }
        public decimal CatalogStockMaximumQuantity { get; private set; }
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogStock() { }

        public static CatalogStock Create(
            int CompanyID,
            int CatalogVariantID,
            int WarehouseID,           
            decimal? CatalogStockCurrentQuantity,
            decimal CatalogStockMinimumQuantity,
            decimal CatalogStockMaximumQuantity,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(CatalogVariantID, CreatedDate, CreatedById);
            return new CatalogStock()
            {
                CompanyID = CompanyID,
                CatalogVariantID = CatalogVariantID,
                WarehouseID = WarehouseID,
                CatalogStockCurrentQuantity = CatalogStockCurrentQuantity,
                CatalogStockMinimumQuantity = CatalogStockMinimumQuantity,
                CatalogStockMaximumQuantity = CatalogStockMaximumQuantity,
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogStock Update(
            int CompanyID,
            int CatalogStockID,
            int CatalogVariantID,
            int WarehouseID, 
            decimal? CatalogStockCurrentQuantity, 
            decimal CatalogStockMinimumQuantity,
            decimal CatalogStockMaximumQuantity,
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(CatalogVariantID, UpdatedDate, UpdatedById);
            return new CatalogStock()
            {
                CompanyID = CompanyID,
                CatalogStockID = CatalogStockID,
                CatalogVariantID = CatalogVariantID,
                WarehouseID = WarehouseID,
                CatalogStockCurrentQuantity = CatalogStockCurrentQuantity,
                CatalogStockMinimumQuantity = CatalogStockMinimumQuantity,
                CatalogStockMaximumQuantity = CatalogStockMaximumQuantity,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogStock ChangeState(int CompanyID, int CatalogStockID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogStock
            { 
                CompanyID = CompanyID,
                CatalogStockID = CatalogStockID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int CatalogVariantID, DateTime CreatedDate, int CreatedById)
        {
            if (CatalogVariantID == 0) throw new ArgumentNullException("El código de variante debe ser mayor a cero" + nameof(CatalogVariantID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El código del usuario debe ser mayor a cero");
        }
    }
}