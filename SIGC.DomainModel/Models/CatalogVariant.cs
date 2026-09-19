using SIGC.DomainModel.Enums;
using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainModel.Models
{
    public class CatalogVariant
    {
        public int CompanyID { get; set; }
        public int CatalogVariantID { get; set; }
        public int CatalogID { get; private set; }
        public string CatalogVariantName { get; private set; }
        public string? CatalogVariantSKU { get; private set; }
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }
        private readonly List<CatalogVariantValue> _CatalogVariantValues = [];
        public IReadOnlyList<CatalogVariantValue> CatalogVariantValues => _CatalogVariantValues.AsReadOnly();

        protected CatalogVariant() { }

        public static CatalogVariant Create(
            int CompanyID,            
            int CatalogID,
            string CatalogVariantName,
            string? CatalogVariantSKU, 
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(CatalogVariantName, CreatedDate, CreatedById);
            return new CatalogVariant()
            {
                CompanyID = CompanyID,
                CatalogID = CatalogID,
                CatalogVariantName = CatalogVariantName,
                CatalogVariantSKU = CatalogVariantSKU,      
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogVariant Update(
            int CompanyID,
            int CatalogVariantID,           
            int CatalogID,
            string CatalogVariantName,
            string? CatalogVariantSKU,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(CatalogVariantName, UpdatedDate, UpdatedById);
            return new CatalogVariant()
            {
                CompanyID = CompanyID,
                CatalogVariantID= CatalogVariantID,
                CatalogID = CatalogID,
                CatalogVariantName = CatalogVariantName,
                CatalogVariantSKU = CatalogVariantSKU,    
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogVariant ChangeState(int CompanyID, int CatalogVariantID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogVariant()
            {
                CompanyID = CompanyID,
                CatalogVariantID = CatalogVariantID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(string CatalogVariantName, DateTime CreatedDate, int CreatedById)
        {
            if (string.IsNullOrWhiteSpace(CatalogVariantName)) throw new ArgumentNullException("El nombre de la variante no debe estar vacia" + nameof(CatalogVariantName));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }

        public void AddCatalogVariantValue(short AttributeValueID)
        {
            _CatalogVariantValues.Add(new CatalogVariantValue(
                CompanyID: this.CompanyID,
                CatalogVariantID: this.CatalogVariantID,
                AttributeValueID: AttributeValueID,    
                RecordOriginID: this.RecordOriginID,
                RecordStateID: this.RecordStateID,
                CreatedById: this.CreatedById,
                CreatedByName: this.CreatedByName,
                CreatedByFullName: this.CreatedByFullName,
                CreatedDate: this.CreatedDate
                )
             );
        }
    }
}