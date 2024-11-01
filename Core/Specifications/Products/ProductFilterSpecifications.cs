using Core.Entities;
using System;
using System.Linq.Expressions;

namespace Core.Specifications.Products
{
    public class ProductFilterSpecifications : BaseSpecifications<Product>
    {
        public ProductFilterSpecifications(Expression<Func<Product, bool>> expression, string[] includes = null)
            : base(expression, includes) { }

        public ProductFilterSpecifications(
            string orderBy = null,
            string name = null,
            int? brandId = null,
            int? typeId = null,
            int? limit = null,
            int? page = null,
            string[] includes = null,
            bool ascending = true)
            : base(BuildCriteria(name, brandId, typeId), includes, orderBy, ascending, limit, page) { }

        // Helper method to build filter criteria expression
        private static Expression<Func<Product, bool>> BuildCriteria(string? name, int? brandId, int? typeId)
        {
            return x =>
                (!brandId.HasValue || x.BrandId == brandId) &&
                (!typeId.HasValue || x.ProductTypeId == typeId) &&
                (string.IsNullOrEmpty(name) || x.Name.Contains(name));
        }
    }
}
