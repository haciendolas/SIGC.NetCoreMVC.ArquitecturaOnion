using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class CatalogPrice
    {
        public int CompanyID { get; set; }
        public int CatalogPriceID { get; set; }
        public int CatalogPresentationID { get; private set; }
        public int EstablishmentID { get; private set; }
        public PriceTypeEnum PriceTypeID { get; private set; }
        public CurrencyTypeEnum CurrencyTypeID { get; private set; }        
        public decimal CatalogPriceAmount { get; private set; }
        public bool CatalogPriceIsTaxIncluded { get; private set; }
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; private set; }
        public string CreatedByFullName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        protected CatalogPrice() { }

        public static CatalogPrice Create(
            int CompanyID,
            int CatalogPresentationID,
            int EstablishmentID,
            PriceTypeEnum PriceTypeID,
            CurrencyTypeEnum CurrencyTypeID,
            decimal CatalogPriceAmount,
            bool CatalogPriceIsTaxIncluded,
            RecordOriginEnum RecordOriginID,
            RecordStateEnum RecordStateID,
            DateTime CreatedDate,
            int CreatedById,
            string CreatedByName,
            string CreatedByFullName
            )
        {
            Validate(CatalogPresentationID, CreatedDate, CreatedById);
            return new CatalogPrice()
            {
                CompanyID = CompanyID,
                CatalogPresentationID = CatalogPresentationID,
                EstablishmentID = EstablishmentID,
                PriceTypeID = PriceTypeID,
                CurrencyTypeID = CurrencyTypeID,
                CatalogPriceAmount = CatalogPriceAmount,
                CatalogPriceIsTaxIncluded = CatalogPriceIsTaxIncluded,
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static CatalogPrice Update(
            int CompanyID,
            int CatalogPriceID,
            int CatalogPresentationID,
            int EstablishmentID,
            PriceTypeEnum PriceTypeID,
            CurrencyTypeEnum CurrencyTypeID,
            decimal CatalogPriceAmount,
            bool CatalogPriceIsTaxIncluded,
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate,
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
            Validate(CatalogPresentationID, UpdatedDate, UpdatedById);
            return new CatalogPrice()
            {
                CompanyID = CompanyID,
                CatalogPriceID = CatalogPriceID,
                CatalogPresentationID = CatalogPresentationID,
                EstablishmentID = EstablishmentID,
                PriceTypeID = PriceTypeID,
                CurrencyTypeID = CurrencyTypeID,
                CatalogPriceAmount = CatalogPriceAmount,
                CatalogPriceIsTaxIncluded= CatalogPriceIsTaxIncluded,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static CatalogPrice ChangeState(int CompanyID, int CatalogPriceID, RecordStateEnum RecordStateID, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {
            return new CatalogPrice
            { 
                CompanyID = CompanyID,
                CatalogPriceID = CatalogPriceID,
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(int CatalogPresentationID, DateTime CreatedDate, int CreatedById)
        {
            if (CatalogPresentationID == 0) throw new ArgumentNullException("El codigo de la presentación debe ser mayo a cero" + nameof(CatalogPresentationID));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}