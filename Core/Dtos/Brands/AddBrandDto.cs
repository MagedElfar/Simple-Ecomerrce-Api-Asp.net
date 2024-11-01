using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Brands
{
    public class AddBrandDto
    {
        [Required]
        public string BrandName { get; set; }
    }
}
