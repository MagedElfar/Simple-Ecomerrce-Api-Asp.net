using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Payments
{
    public class PaymentConfirmationDto
    {
        [Required]
        public int OrderId { get; set; }
    }
}
