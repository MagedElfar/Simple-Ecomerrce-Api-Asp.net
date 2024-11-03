using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SpecificationsEvaluate<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query , ISpecifications<TEntity> specifications) {
            var newQuery = query;

            if (specifications.Criteria is not null)
            {
                newQuery = newQuery.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null) {
                newQuery = newQuery.OrderBy(specifications.OrderBy);
            } else if (specifications.OrderByDesc is not null)
            {
                newQuery = newQuery.OrderByDescending(specifications.OrderByDesc);
            }

            if (specifications.IsPagingEnabled == true) {
                newQuery = newQuery.Skip(specifications.Skip)
                    .Take(specifications.Take);
            }

            //newQuery = specifications.Includes.Aggregate(newQuery, (crruent, include) => crruent.Include(include));

            foreach (var include in specifications.Includes)
            {
                newQuery = newQuery.Include(include);
            }
            return newQuery;
        }
    }
}
