using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class Catalog
    {
        public int CompanyID { get; set; }
        public int CatalogID { get; set; }
        public byte CatalogTypeID { get; private set; }
        public int CategoryID { get; private set; }
        public string? CatalogCode { get; private set; }
        public string CatalogSlug { get; private set; }
        public string CatalogName { get; private set; }
        public byte? PrescriptionTypeID { get; private set; }
        public int? ManufacturerID { get; private set; }
        public int? BrandID { get; private set; }
        public short? PharmaceuticalFormID { get; private set; }
        public string CatalogBrandType { get; private set; }
        public string? CatalogDescription { get; private set; }
        public string? CatalogImage { get; private set; }
        public RecordOriginEnum RecordOriginID{ get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }     
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected Catalog() { }

        public static Catalog Create(
            int CompanyID,
            byte CatalogTypeID,
            int CategoryID,
            string? CatalogCode,
            string CatalogSlug,
            string CatalogName,            
            byte? PrescriptionTypeID,
            int? ManufacturerID,
            int? BrandID,
            short? PharmaceuticalFormID,
            string CatalogBrandType,
            string? CatalogDescription,
            string? CatalogImage,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
          Validate(CatalogName, CreatedDate, CreatedById);
            return new Catalog()
            {
                CompanyID = CompanyID,
                CatalogTypeID = CatalogTypeID,
                CategoryID = CategoryID,
                CatalogCode = CatalogCode,
                CatalogSlug = CatalogSlug,
                CatalogName = CatalogName,
                PrescriptionTypeID = PrescriptionTypeID,
                ManufacturerID = ManufacturerID,
                BrandID = BrandID,
                PharmaceuticalFormID = PharmaceuticalFormID,
                CatalogBrandType = CatalogBrandType,
                CatalogDescription = CatalogDescription,
                CatalogImage = CatalogImage,
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static Catalog Update(         
            int CompanyID,
            int CatalogID,
            byte CatalogTypeID,
            int CategoryID,
            string? CatalogCode,
            string CatalogSlug,
            string CatalogName,
            byte? PrescriptionTypeID,
            int? ManufacturerID,
            int? BrandID,
            short? PharmaceuticalFormID,
            string CatalogBrandType,
            string? CatalogDescription,
            string? CatalogImage,
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
             Validate(CatalogName, UpdatedDate, UpdatedById);
            return new Catalog()
            {
                CompanyID= CompanyID,
                CatalogID = CatalogID,
                CatalogTypeID = CatalogTypeID,
                CategoryID = CategoryID,
                CatalogCode = CatalogCode,
                CatalogSlug = CatalogSlug,
                CatalogName = CatalogName,
                PrescriptionTypeID = PrescriptionTypeID,
                ManufacturerID = ManufacturerID,
                BrandID = BrandID,
                PharmaceuticalFormID = PharmaceuticalFormID,
                CatalogBrandType = CatalogBrandType,
                CatalogDescription = CatalogDescription,
                CatalogImage = CatalogImage,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName= UpdatedByName,
                CreatedByFullName= UpdatedByFullName
            };
        }

        public static Catalog ChangeState(int CompanyID, int CatalogID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {            
            return new Catalog()
            {
                CompanyID = CompanyID,
                CatalogID = CatalogID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName   = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(string CatalogName, DateTime CreatedDate, int CreatedById)
        {
            if (string.IsNullOrWhiteSpace(CatalogName)) throw new ArgumentNullException("El nombre de la catálogo no debe estar vacia" + nameof(CatalogName));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById==0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}
