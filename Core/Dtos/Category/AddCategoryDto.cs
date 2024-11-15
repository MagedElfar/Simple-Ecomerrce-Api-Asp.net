using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Category
{
    public class AddCatwgoryDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
