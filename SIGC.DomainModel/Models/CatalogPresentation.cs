using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class CatalogPresentation
    {
        public int CompanyID { get; set; }
        public int CatalogPresentationID { get; set; }
        public int CatalogVariantID { get; private set; }
        public int PresentationID { get; private set; }
        public bool CatalogPresentationIsDefault { get; private set; }
        public decimal CatalogPresentationEquivalence { get; private set; }        
        public string? CatalogPresentationSKU { get; private set; }
        public string? CatalogPresentationBarcode { get; private set; }
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogPresentation() { }

        public static CatalogPresentation Create(
            int CompanyID,
            int CatalogVariantID,
            int PresentationID,
            bool CatalogPresentationIsDefault,
            decimal CatalogPresentationEquivalence,
            string? CatalogPresentationSKU,
            string? CatalogPresentationBarcode,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(PresentationID, CreatedDate, CreatedById);
            return new CatalogPresentation()
            {
                CompanyID = CompanyID,
                CatalogVariantID = CatalogVariantID,
                PresentationID = PresentationID,
                CatalogPresentationIsDefault = CatalogPresentationIsDefault,
                CatalogPresentationEquivalence = CatalogPresentationEquivalence,
                CatalogPresentationSKU = CatalogPresentationSKU,
                CatalogPresentationBarcode = CatalogPresentationBarcode,
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogPresentation Update(
            int CompanyID,
            int CatalogPresentationID,
            int CatalogVariantID,
            int PresentationID, 
            decimal CatalogPresentationEquivalence,
            string? CatalogPresentationSKU,
            string? CatalogPresentationBarcode,
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(PresentationID, UpdatedDate, UpdatedById);
            return new CatalogPresentation()
            {
                CompanyID = CompanyID,
                CatalogPresentationID = CatalogPresentationID,
                CatalogVariantID = CatalogVariantID,
                PresentationID = PresentationID, 
                CatalogPresentationEquivalence = CatalogPresentationEquivalence,
                CatalogPresentationBarcode = CatalogPresentationBarcode,
                CatalogPresentationSKU = CatalogPresentationSKU,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogPresentation ChangeState(int CompanyID, int CatalogPresentationID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogPresentation { 
                CompanyID = CompanyID,
                CatalogPresentationID = CatalogPresentationID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int PresentationID, DateTime CreatedDate, int CreatedById)
        {
            if (PresentationID == 0) throw new ArgumentNullException("El codigo de la presentación debe ser mayo a cero" + nameof(PresentationID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}