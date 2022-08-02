namespace WebApiProductCRUD.Areas.Web.ViewModels
{
    /// <summary>
    /// <see cref="WebApiProductCRUD.Models.Product" />
    /// </summary>
    public class ProductVm
    {
        public string? Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateUtc { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }

        public string? Name { get; set; }
        public double Price { get; set; }

        public string? DisplayPrice { get; set; }

        private double _stock;
        public double Stock
        {
            get => _stock;
            set => _stock = value > 0 ? value : 0;
        }
    }
}
