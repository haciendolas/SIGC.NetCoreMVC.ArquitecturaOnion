using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
    public class CatalogTaxExemption
    {
        public int CompanyID { get; set; }
        public int CatalogTaxExemptionID { get; set; }
        public int EstablishmentID { get; private set; }
        public int CatalogTaxID { get; private set; }     
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogTaxExemption() { }

        public static CatalogTaxExemption Create(
            int CompanyID,
            int EstablishmentID,
            int CatalogTaxID,       
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(EstablishmentID, CreatedDate, CreatedById);
            return new CatalogTaxExemption()
            {
                CompanyID = CompanyID,
                EstablishmentID = EstablishmentID,
                CatalogTaxID = CatalogTaxID,                       
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogTaxExemption Update(
            int CompanyID,
            int CatalogTaxExemptionID,
            int EstablishmentID,           
            int CatalogTaxID,           
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(EstablishmentID, UpdatedDate, UpdatedById);
            return new CatalogTaxExemption()
            {
                CompanyID = CompanyID,
                CatalogTaxExemptionID = CatalogTaxExemptionID,
                EstablishmentID = EstablishmentID,
                CatalogTaxID = CatalogTaxID,                           
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogTaxExemption ChangeState(int CompanyID, int CatalogTaxExemptionID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogTaxExemption
            { 
                CompanyID = CompanyID,
                CatalogTaxExemptionID = CatalogTaxExemptionID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int EstablishmentID, DateTime CreatedDate, int CreatedById)
        {
            if (EstablishmentID == 0) throw new ArgumentNullException("El código del establecimiento debe ser mayor a cero" + nameof(EstablishmentID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El código del usuario debe ser mayor a cero");
        }
    }
}