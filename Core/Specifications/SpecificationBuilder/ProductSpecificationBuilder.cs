using Product = Core.Entities.Product;
using Core.Extensions;
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
        public ProductSpecificationBuilder WithName(string name)
        {

            _criteria = PredicateBuilder.And(_criteria, p => p.Name.Contains(name));
            
            return this;
        }
        public ProductSpecificationBuilder WithBrandId(int? brandId)
        {
          
            _criteria = PredicateBuilder.And(_criteria, p => p.BrandId == brandId);
            return this;
        }
        public ProductSpecificationBuilder WithTypeId(int? typeId)
        {
            _criteria = PredicateBuilder.And(_criteria, p => p.CategoryId == typeId);
            return this;
        }
        
    }
}
