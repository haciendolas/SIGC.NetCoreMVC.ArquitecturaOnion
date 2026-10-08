using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
    public class CatalogTax
    {
        public int CompanyID { get; set; }
        public int CatalogTaxID { get; set; }
        public int CatalogID { get; private set; }
        public short TaxID { get; private set; }
        public byte TaxAffectationTypeID { get; private set; }       
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogTax() { }

        public static CatalogTax Create(
            int CompanyID,
            int CatalogID,
            short TaxID,
            byte TaxAffectationTypeID,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(CatalogID, CreatedDate, CreatedById);
            return new CatalogTax()
            {
                CompanyID = CompanyID,
                CatalogID = CatalogID,
                TaxID = TaxID,
                TaxAffectationTypeID = TaxAffectationTypeID,               
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogTax Update(
            int CompanyID,
            int CatalogTaxID,
            int CatalogID,           
            short TaxID,
            byte TaxAffectationTypeID, 
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(CatalogID, UpdatedDate, UpdatedById);
            return new CatalogTax()
            {
                CompanyID = CompanyID,
                CatalogTaxID = CatalogTaxID,
                CatalogID = CatalogID,
                TaxID = TaxID,
                TaxAffectationTypeID = TaxAffectationTypeID,               
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogTax ChangeState(int CompanyID, int CatalogTaxID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogTax
            { 
                CompanyID = CompanyID,
                CatalogTaxID = CatalogTaxID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int CatalogID, DateTime CreatedDate, int CreatedById)
        {
            if (CatalogID == 0) throw new ArgumentNullException("El código de catálogo debe ser mayo a cero" + nameof(CatalogID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El código del usuario debe ser mayor a cero");
        }
    }
}