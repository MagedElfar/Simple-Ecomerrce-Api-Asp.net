using Product = Core.Entities.Product;
using Core.Extensions;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using Core.Helper;

namespace Core.Specifications.SpecificationBuilder
{
        // Builder class for constructing ProductFilterSpecifications
    public class ProductSpecificationBuilder:BaseSpecificationBuilder<Product , ProductSpecificationBuilder>
    {
        public ProductSpecificationBuilder WithName(string? name)
        {

            if (!string.IsNullOrEmpty(name)) {
                _criteria = PredicateBuilder.And(_criteria, p => p.Name.Contains(name));
            }
            return this;
        }
        public ProductSpecificationBuilder WithBrandId(int? brandId)
        {
            if (brandId != null)
            {

                _criteria = PredicateBuilder.And(_criteria, p => p.BrandId == brandId);
            };
            return this;
        }
        public ProductSpecificationBuilder WithTypeId(int? typeId)
        {
            if(typeId != null)
            {
                _criteria = PredicateBuilder.And(_criteria, p => p.ProductTypeId == typeId);
            };
            return this;
        }
        
    }
}
