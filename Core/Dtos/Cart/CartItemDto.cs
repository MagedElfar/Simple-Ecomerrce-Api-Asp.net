using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Cart
{
    public class CartItemDto
    {
        [Required]
        public int ProductId {  get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity must be grater than 0")]
        public int Quantity { get; set; }
    }
}