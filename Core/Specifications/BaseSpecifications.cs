using Core.Interfaces.Services;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        public BaseSpecifications(
            Expression<Func<T, bool>> criteria = null,
            string[] includes = null,
            string orderBy = null,
            bool ascending = true,
            int take = 0,
            int skip = 0
        ){
            Criteria = criteria;
            ApplyIncludes(includes);
            ApplySorting(orderBy, ascending);
            ApplyPaging(take, skip);
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<string> Includes { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; set; }

        // Apply methods
        protected void ApplySorting(string propertyName, bool ascending = true)
        {
            if (string.IsNullOrEmpty(propertyName)) return;

            ValidateProperty(propertyName);

            if (ascending)
                AddOrderBy(propertyName);
            else
                AddOrderByDesc(propertyName);
        }

        protected void ApplyIncludes(string[] includes = null)
        {
            if (includes == null) return;

            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        protected void ApplyPaging(int limit, int page)
        {
            if (limit > 0 && page > 0)
            {
                Take = limit;
                Skip = (page - 1) * limit; // Ensure Skip is zero-based.
                IsPagingEnabled = true;
            }
        }

        // Add methods
        protected virtual void AddInclude(string include)
        {
            ValidateProperty(include);
            Includes.Add(include);
        }

        protected virtual void AddOrderBy(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var orderByExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            OrderBy = orderByExpression;
        }

        protected virtual void AddOrderByDesc(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var orderByDescExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            OrderByDesc = orderByDescExpression;
        }

        protected void ValidateProperty(string propertyName)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T).Name}'");
            }
        }
    }
}
