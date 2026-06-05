using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class Category
    {
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; private set; } = null!;
        public string CategorySlug { get; private set; } = null!;
        public string? CategoryImage { get; set; }
        public RecordOriginEnum RecordOriginId{ get; private set; }
        public RecordStateEnum RecordStateId { get; private set; }     
        public int CreatedById { get; private set; }
        public string CreatedByName { get; set; }
        public string CreatedByFullName { get; set; }
        public DateTime CreatedDate { get; private set; }

        protected Category() { }

        public static Category Create(int CompanyId,string CategoryName,string CategorySlug,string? CategoryImage, RecordStateEnum RecordStateId, DateTime CreatedDate,int CreatedById,
            string CreatedByName,string CreatedByFullName
            )
        {
            Validate(CategoryName, CreatedDate, CreatedById);
            return new Category()
            {
                CompanyId = CompanyId,
                CategoryName = CategoryName,
                CategorySlug  = CategorySlug,
                CategoryImage = CategoryImage,
                RecordStateId = RecordStateId,
                CreatedDate = CreatedDate,
                CreatedById = CreatedById,
                CreatedByName = CreatedByName,
                CreatedByFullName = CreatedByFullName
            };
        }

        public static Category Update(int CompanyId, int CategoryId, string CategoryName, string CategorySlug, string? CategoryImage, RecordStateEnum RecordStateId, DateTime UpdatedDate, int UpdatedById,
            string UpdatedByName, string UpdatedByFullName)
        {
            Validate(CategoryName, UpdatedDate, UpdatedById);
            return new Category()
            {
                CompanyId= CompanyId,
                CategoryId = CategoryId,
                CategoryName = CategoryName,
                CategorySlug = CategorySlug,
                CategoryImage = CategoryImage,
                RecordStateId = RecordStateId,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName= UpdatedByName,
                CreatedByFullName= UpdatedByFullName
            };
        }

        public static Category ChangeState(int CompanyId, int CategoryId,RecordStateEnum RecordStateId, DateTime UpdatedDate, int UpdatedById, string UpdatedByName, string UpdatedByFullName)
        {            
            return new Category()
            {
                CompanyId = CompanyId,
                CategoryId = CategoryId,
                RecordStateId = RecordStateId,
                CreatedDate = UpdatedDate,
                CreatedById = UpdatedById,
                CreatedByName   = UpdatedByName,
                CreatedByFullName = UpdatedByFullName
            };
        }

        private static void Validate(string CategoryName, DateTime CreatedDate, int CreatedById)
        {
            if (string.IsNullOrWhiteSpace(CategoryName)) throw new ArgumentNullException("El nombre de la categoria no debe estar vacia" + nameof(CategoryName));
            if (CreatedDate.AddMinutes(1) < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedById==0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}
