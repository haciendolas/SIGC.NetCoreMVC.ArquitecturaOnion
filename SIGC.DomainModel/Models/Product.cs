using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
   public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; private set; }
        public string ProductCode { get; private set; } = null!;
        public string ProductName { get; private set; } = null!;
        public StateEnum StateId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int CreatedBy { get; private set; }

        protected Product() { }

        public static Product Create(int CategoryId,string ProductCode,string ProductName, StateEnum StateId, DateTime CreatedDate,int CreatedBy) {

            Validate(CategoryId, ProductCode, ProductName, CreatedDate, CreatedBy);
            return new Product {
                CategoryId = CategoryId,
                ProductCode = ProductCode,
                ProductName = ProductName,
                StateId = StateId,
                CreatedDate = CreatedDate,
                CreatedBy = CreatedBy
            };
        }

        public static Product Update(int ProductId, int CategoryId, string ProductCode, string ProductName, StateEnum StateId, DateTime UpdatedDate, int UpdatedBy)
        {
            Validate(CategoryId, ProductCode, ProductName, UpdatedDate, UpdatedBy);
            return new Product
            {
                ProductId = ProductId,
                CategoryId = CategoryId,
                ProductCode = ProductCode,
                ProductName = ProductName,
                StateId = StateId,
                CreatedDate = UpdatedDate,
                CreatedBy = UpdatedBy
            };
        }

        private static void Validate(int CategoryId, string ProductCode, string ProductName,  DateTime CreatedDate, int CreatedBy)
        {
            if (CategoryId == 0) throw new ArgumentException("La categoria es obligatoria");
            if (string.IsNullOrWhiteSpace(ProductCode)) throw new ArgumentException("El código del producto es obligatorio");
            if (string.IsNullOrWhiteSpace(ProductName)) throw new ArgumentException("El nombre del producto es obligatorio");
        }
    }
}