using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{ 
    public class Establishment
    {
        public int CompanyID { get; set; }
        public int EstablishmentID { get; set; }
        public int PersonID { get; private set; }
        public byte TypeID { get; set; }
        public string EstablishmentCode { get; private set; } = null!;
        public string EstablishmentName { get; private set; } = null!;
        public string EstablishmentAddress { get; private set; } = null!;
        public string? EstablishmentLogo { get; set; }
        public RecordOriginEnum RecordOriginId { get; private set; }
        public RecordStateEnum RecordStateId { get; private set; }
        public int CreatedById { get; private set; }
        public string CreatedByName { get; set; }
        public string CreatedByFullName { get; set; }
        public DateTime CreatedDate { get; private set; }

        protected Establishment() { }

        public static Establishment Create(
                                int CompanyID,
                                int PersonID,
                                byte TypeID, 
                                string EstablishmentCode, 
                                string EstablishmentName,
                                string EstablishmentAddress, 
                                string? EstablishmentLogo,
                                RecordOriginEnum RecordOriginId,
                                RecordStateEnum RecordStateId,
                                DateTime CreatedDate,
                                int CreatedById,
                                string CreatedByName, 
                                string CreatedByFullName
            )
        {
           // Validate(CategoryName, CreatedDate, CreatedById);
            return new Establishment()
            {
                CompanyID = CompanyID,
                PersonID = PersonID,
                TypeID = TypeID,
                EstablishmentCode = EstablishmentCode,
                EstablishmentName = EstablishmentName,
                EstablishmentAddress = EstablishmentAddress,
                EstablishmentLogo = EstablishmentLogo,
                RecordOriginId = RecordOriginId,
                RecordStateId = RecordStateId,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static Establishment Update(
            int CompanyID, 
            int EstablishmentID, 
            int PersonID,
            byte TypeID,
            string EstablishmentCode, 
            string EstablishmentName, 
            string EstablishmentAddress,
            string? EstablishmentLogo,
            RecordStateEnum RecordStateId,
            DateTime UpdatedDate, 
            int UpdatedById,
            string UpdatedByName,
            string UpdatedByFullName)
        {
           // Validate(CategoryName, UpdatedDate, UpdatedById);
            return new Establishment()
            {
                CompanyID = CompanyID,
                EstablishmentID = EstablishmentID,
                PersonID = PersonID,
                TypeID = TypeID,
                EstablishmentCode = EstablishmentCode,
                EstablishmentName = EstablishmentName,
                EstablishmentAddress = EstablishmentAddress,
                EstablishmentLogo = EstablishmentLogo,
                RecordStateId = RecordStateId,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        public static Establishment ChangeState(
             int CompanyID,
             int EstablishmentID,     
             RecordStateEnum RecordStateId,
             DateTime UpdatedDate, 
             int UpdatedById, 
             string UpdatedByName,
             string UpdatedByFullName)
        {
            return new Establishment()
            {
                CompanyID = CompanyID,
                EstablishmentID = EstablishmentID,                
                RecordStateId = RecordStateId,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(string CategoryName, DateTime CreatedDate, int CreatedById)
        {
            if (string.IsNullOrWhiteSpace(CategoryName)) throw new ArgumentNullException("El nombre de la categoria no debe estar vacia" + nameof(CategoryName));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}
