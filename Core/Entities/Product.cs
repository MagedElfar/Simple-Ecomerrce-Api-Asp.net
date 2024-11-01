namespace Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public string SKU { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? imageUrl { get; set; }

        public int? ProductTypeId { get; set; }

        public ProductType? ProductType { get; set; }

        public int? BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
