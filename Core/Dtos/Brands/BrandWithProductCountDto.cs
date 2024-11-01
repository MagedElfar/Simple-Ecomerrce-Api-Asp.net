using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Brands
{
    public class BrandWithProductCountDto
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int ProductsCount { get; set; }
    }
}
