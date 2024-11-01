using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        [Range( 1 , int.MaxValue , ErrorMessage = "Paymentmethod id should be graterthan 0")]
        public int PaymentMethodId { get; set; }
    }
}
