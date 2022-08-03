using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProductCRUD.Models
{
    public class Product : IAppEntity
    {
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public string? Id { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateUtc { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }

        public string? Name { get; set; }

        [NotMapped]
        private double _price;
        public double Price
        {
            get => _price;
            set => _price = value > 0 ? value : 0;
        }

        public double Stock { get; set; }
    }
}
