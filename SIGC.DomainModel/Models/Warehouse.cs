using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{ 
    public class Warehouse
    {
        public int CompanyID { get; set; }
        public int WarehouseID  { get; set; }
        public int EstablishmentID { get; private set; }
        public byte WarehouseTypeID { get; set; }
        public string WarehouseCode { get; private set; } = null!;
        public string WarehouseName { get; private set; } = null!;
        public string WarehouseAddress { get; private set; } = null!;       
        public RecordOriginEnum RecordOriginID { get; private set; }
        public RecordStateEnum RecordStateID { get; private set; }
        public int CreatedByID { get; private set; }
        public string CreatedByName { get; set; }
        public string CreatedByFullName { get; set; }
        public DateTime CreatedDate { get; private set; }

        protected Warehouse() { }

        public static Warehouse Create(
                                int CompanyID,
                                int EstablishmentID,
                                byte WarehouseTypeID, 
                                string WarehouseCode, 
                                string WarehouseName,
                                string WarehouseAddress,                       
                                RecordOriginEnum RecordOriginID,
                                RecordStateEnum RecordStateID,
                                DateTime CreatedDate,
                                int CreatedByID,
                                string CreatedByName, 
                                string CreatedByFullName
            )
        {
           // Validate(CategoryName, CreatedDate, CreatedById);
            return new Warehouse()
            {
                CompanyID = CompanyID,
                EstablishmentID = EstablishmentID,
                WarehouseTypeID = WarehouseTypeID,
                WarehouseCode = WarehouseCode,
                WarehouseName = WarehouseName,
                WarehouseAddress = WarehouseAddress,                
                RecordOriginID = RecordOriginID,
                RecordStateID = RecordStateID,
                CreatedDate = CreatedDate,
                CreatedByID = CreatedByID,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static Warehouse Update(
            int CompanyID, 
            int WarehouseID, 
            int EstablishmentID,
            byte WarehouseTypeID,
            string WarehouseCode, 
            string WarehouseName, 
            string WarehouseAddress,        
            RecordStateEnum RecordStateID,
            DateTime UpdatedDate, 
            int UpdatedByID,
            string UpdatedByName,
            string UpdatedByFullName)
        {
           // Validate(CategoryName, UpdatedDate, UpdatedById);
            return new Warehouse()
            {
                CompanyID = CompanyID,
                WarehouseID = WarehouseID,
                EstablishmentID = EstablishmentID,
                WarehouseTypeID = WarehouseTypeID,
                WarehouseCode = WarehouseCode,
                WarehouseName = WarehouseName,
                WarehouseAddress = WarehouseAddress,             
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedByID = UpdatedByID,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static Warehouse ChangeState(
             int CompanyID,
             int WarehouseID,     
             RecordStateEnum RecordStateID,
             DateTime UpdatedDate, 
             int UpdatedByID, 
             string UpdatedByName,
             string UpdatedByFullName)
        {
            return new Warehouse()
            {
                CompanyID = CompanyID,
                WarehouseID = WarehouseID,                
                RecordStateID = RecordStateID,
                CreatedDate = UpdatedDate,
                CreatedByID = UpdatedByID,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(string WarehouseName, DateTime CreatedDate, int CreatedByID)
        {
            if (string.IsNullOrWhiteSpace(WarehouseName)) throw new ArgumentNullException("El nombre del almacen no debe estar vacia" + nameof(WarehouseName));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedByID == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}
