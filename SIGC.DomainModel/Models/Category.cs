using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; private set; } = null!;
        public StateEnum StateId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int CreatedBy { get; private set; }

        protected Category() { }

        public static Category Create(string CategoryName, StateEnum StateId,DateTime CreatedDate,int CreatedBy)
        {
            Validate(CategoryName, CreatedDate, CreatedBy);
            return new Category()
            {
                CategoryName = CategoryName,
                StateId = StateId,
                CreatedDate = CreatedDate,
                CreatedBy = CreatedBy
            };
        }

        public static Category Update(int CategoryId, string CategoryName, StateEnum StateId, DateTime UpdatedDate, int UpdatedBy)
        {
            Validate(CategoryName, UpdatedDate, UpdatedBy);
            return new Category()
            {
                CategoryId = CategoryId,
                CategoryName = CategoryName,
                StateId = StateId,
                CreatedDate = UpdatedDate,
                CreatedBy = UpdatedBy
            };
        }

        private static void Validate(string CategoryName, DateTime CreatedDate, int CreatedBy)
        {
            if (string.IsNullOrWhiteSpace(CategoryName)) throw new ArgumentNullException("El nombre de la categoria no debe estar vacia" + nameof(CategoryName));
            if (CreatedDate < DateTime.Now) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedBy==0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}
