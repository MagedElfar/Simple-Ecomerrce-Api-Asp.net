using Core.Dtos.Products;
using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<EntityWithCount<T>> FindAndCountAll(ISpecifications<T> specifications);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(ISpecifications<T> specifications);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindByIdAsync(int id);
        Task<T?> FindOneAync(ISpecifications<T> specifications);
        Task<T?> FindOneAync(Expression<Func<T , bool>> predicate);
        Task<T> CreateAync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(ISpecifications<T> specifications);
    }
}
