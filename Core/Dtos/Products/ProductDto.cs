using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Products
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product SKU is required.")]
        [StringLength(50, ErrorMessage = "Product SKU can't be longer than 50 characters.")]
        public string ProductSKU { get; set; }

        [StringLength(500, ErrorMessage = "Product description can't be longer than 500 characters.")]
        public string? ProductDescription { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
        public decimal ProductPrice { get; set; }

        public string? Image { get; set; }

        public string? CategoryName { get; set; }

        public string? BrandName { get; set; }    }
}
