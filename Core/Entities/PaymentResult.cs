using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PaymentResult
    {
        public string? PaymentIntentId {  get; set; }

        public string? ClientSecret { get; set; }

        public string Status {  get; set; }
    }
}
