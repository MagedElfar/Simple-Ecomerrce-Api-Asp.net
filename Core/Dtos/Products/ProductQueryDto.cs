using Core.Dtos.Pagning;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Products
{
    public class ProductQueryDto : BaseSearchQueryDto
    {
        public string? Sort {  get; set; }

        public bool? Asc {  get; set; } = true;

        public string? Name { get; set; }

        public int? BrandId {  get; set; }
        public int? CategoryId { get; set; }

    }
}
