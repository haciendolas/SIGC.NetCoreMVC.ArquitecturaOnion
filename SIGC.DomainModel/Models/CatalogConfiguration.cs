using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
    public class CatalogConfiguration
    {
        public int CompanyID { get; set; }
        public int CatalogConfigurationID { get; set; }
        public int EstablishmentID { get; private set; }
        public int CatalogID { get; private set; }
        public bool CatalogConfigurationIsStockManaged { get; private set; }
        public bool CatalogConfigurationIsAffectStock { get; private set; }        
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogConfiguration() { }

        public static CatalogConfiguration Create(
            int CompanyID,
            int EstablishmentID,
            int CatalogID,           
            bool CatalogConfigurationIsStockManaged,
            bool CatalogConfigurationIsAffectStock,           
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(EstablishmentID, CreatedDate, CreatedById);
            return new CatalogConfiguration()
            {
                CompanyID = CompanyID,
                EstablishmentID = EstablishmentID,
                CatalogID = CatalogID,
                CatalogConfigurationIsStockManaged = CatalogConfigurationIsStockManaged,
                CatalogConfigurationIsAffectStock = CatalogConfigurationIsAffectStock,               
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogConfiguration Update(
            int CompanyID,
            int CatalogConfigurationID,
            int EstablishmentID,
            int CatalogID, 
            bool CatalogConfigurationIsStockManaged,
            bool CatalogConfigurationIsAffectStock,          
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(EstablishmentID, UpdatedDate, UpdatedById);
            return new CatalogConfiguration()
            {
                CompanyID = CompanyID,
                CatalogConfigurationID = CatalogConfigurationID,
                EstablishmentID = EstablishmentID,
                CatalogID = CatalogID,               
                CatalogConfigurationIsStockManaged = CatalogConfigurationIsStockManaged,
                CatalogConfigurationIsAffectStock = CatalogConfigurationIsAffectStock,                
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogConfiguration ChangeState(int CompanyID, int CatalogConfigurationID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogConfiguration
            { 
                CompanyID = CompanyID,
                CatalogConfigurationID = CatalogConfigurationID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int EstablishmentID, DateTime CreatedDate, int CreatedById)
        {
            if (EstablishmentID == 0) throw new ArgumentNullException("El código de establecimiento debe ser mayor a cero" + nameof(EstablishmentID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El código del usuario debe ser mayor a cero");
        }
    }
}