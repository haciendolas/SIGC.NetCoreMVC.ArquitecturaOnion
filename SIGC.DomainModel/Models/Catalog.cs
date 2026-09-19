using SIGC.DomainModel.Enums;
using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainModel.Models
{
   public class Catalog {
        public int CompanyID { get; set; }
        public int CatalogID { get; set; }
        public byte CatalogTypeID { get; private set; }
        public int CategoryID { get; private set; }
        public string? CatalogCode { get; private set; }
        public string CatalogSlug { get; private set; }
        public string CatalogName { get; private set; }
        public bool CatalogHasVariants { get; private set; }
        public byte SaleConditionID { get; private set; }
        public int? ManufacturerID { get; private set; }
        public int? BrandID { get; private set; }
        public short? PharmaceuticalFormID { get; private set; }
        public string CatalogBrandType { get; private set; }
        public string? CatalogConcentration { get; set; }
        public string? CatalogDescription { get; private set; }
        public string? CatalogImage { get; private set; }
        public RecordOriginEnum RecordOriginID{ get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }     
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; } 
     
        private readonly List<CatalogActiveIngredient> _CatalogActiveIngredients = [];
        public IReadOnlyList<CatalogActiveIngredient> CatalogActiveIngredients => _CatalogActiveIngredients.AsReadOnly();

        private readonly List<CatalogTherapeuticAction> _CatalogTherapeuticActions = [];
        public IReadOnlyList<CatalogTherapeuticAction> CatalogTherapeuticActions => _CatalogTherapeuticActions.AsReadOnly();

        protected Catalog() { }

        public static Catalog Create(
            int CompanyID,
            byte CatalogTypeID,
            int CategoryID,
            string? CatalogCode,
            string CatalogSlug,
            string CatalogName, 
            bool CatalogHasVariants,
            byte SaleConditionID,
            int? ManufacturerID,
            int? BrandID,
            short? PharmaceuticalFormID,
            string CatalogBrandType,
            string? CatalogConcentration,
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
                CatalogHasVariants = CatalogHasVariants,
                SaleConditionID = SaleConditionID,
                ManufacturerID = ManufacturerID,
                BrandID = BrandID,
                PharmaceuticalFormID = PharmaceuticalFormID,
                CatalogBrandType = CatalogBrandType,
                CatalogConcentration = CatalogConcentration,
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
            bool CatalogHasVariants,
            byte SaleConditionID,
            int? ManufacturerID,
            int? BrandID,
            short? PharmaceuticalFormID,
            string CatalogBrandType,
            string? CatalogConcentration,
            string? CatalogDescription,
            string? CatalogImage,
            RecordOriginEnum RecordOriginID,
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
                CatalogHasVariants = CatalogHasVariants,
                SaleConditionID = SaleConditionID,
                ManufacturerID = ManufacturerID,
                BrandID = BrandID,
                PharmaceuticalFormID = PharmaceuticalFormID,
                CatalogBrandType = CatalogBrandType,
                CatalogConcentration = CatalogConcentration,
                CatalogDescription = CatalogDescription,
                CatalogImage = CatalogImage,
                RecordOriginID = RecordOriginID,
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

        public void AddCatalogActiveIngredient(            
            int ActiveIngredientID,
            decimal? CatalogActiveIngredientQuantity,            
            int? UnitMeasureID,
            string? CatalogActiveIngredientLabel)
        {
            _CatalogActiveIngredients.Add(new CatalogActiveIngredient(
                CompanyID:this.CompanyID,
                CatalogID:this.CatalogID,
                ActiveIngredientID: ActiveIngredientID,               
                CatalogActiveIngredientQuantity: CatalogActiveIngredientQuantity,
                UnitMeasureID: UnitMeasureID,
                CatalogActiveIngredientLabel: CatalogActiveIngredientLabel,
                RecordOriginID: this.RecordOriginID,
                RecordStateID:this.RecordStateID,
                CreatedById:this.CreatedById,
                CreatedByName:this.CreatedByName,
                CreatedByFullName:this.CreatedByFullName,
                CreatedDate:this.CreatedDate
                )
             );
        }

        public void AddCatalogTherapeuticAction(short TherapeuticActionID)
        {
            _CatalogTherapeuticActions.Add(new CatalogTherapeuticAction(
                CompanyID: this.CompanyID,
                CatalogID: this.CatalogID,
                TherapeuticActionID: TherapeuticActionID,       
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
