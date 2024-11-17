using Core.DTOS.Shared;
using Core.Specifications.SpecificationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Product
{
    public class ProductQueryDto:BaseSearchQueryDto
    {
        public string? Sort { get; set; }

        public bool? Asc { get; set; } = true;

        public string? Name { get; set; }

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }


        public ProductSpecificationBuilder BuildSpecification()
        {
            var builder = new ProductSpecificationBuilder();

            if (!string.IsNullOrEmpty(Name))
            {
                builder.WithName(Name);
            }

            if (!string.IsNullOrEmpty(Sort))
            {
                builder.WithOrderBy(Sort, Asc ?? true);
            }

            if (CategoryId != null)
            {
                builder.WithTypeId(CategoryId);

            }

            if (BrandId != null)
            {
                builder.WithBrandId(BrandId);
            }

            return builder;
        }
    }
}
