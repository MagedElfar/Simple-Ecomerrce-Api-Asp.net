namespace Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public string SKU { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public int? BrandId { get; set; }

        public Brand? Brand { get; set; }

        public int Quantity { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
