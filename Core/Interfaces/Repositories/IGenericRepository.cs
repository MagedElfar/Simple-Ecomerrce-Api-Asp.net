using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetOneAsync(ISpecifications<T> specifications);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(ISpecifications<T> specifications);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(ISpecifications<T> specifications);
    }
}
