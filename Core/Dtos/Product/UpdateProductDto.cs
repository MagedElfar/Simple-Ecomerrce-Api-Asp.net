using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Product
{
    public class UpdateProductDto
    {
        public int? CategoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Brand id must be greater than zero.")]
        public int? BrandId { get; set; }
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
    }
}
