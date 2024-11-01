using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using System.Linq.Expressions;
using System.Reflection;


namespace Infrastructure.Services
{
    public class BaseSerivce<T>:IBaseService<T> where T : BaseEntity
    {
        private readonly  IGenericRepository<T> repository;

        public BaseSerivce(
          IGenericRepository<T> repository
        ){
            this.repository = repository;
        }

        // Get methods

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await repository.GetAllAsync();
        }
        public virtual async Task<IEnumerable<T>> FindAllAsync(ISpecifications<T> specifications)
        {
            return await repository.GetAllAsync(specifications);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.GetAllAsync(predicate);
        }

        public virtual async Task<EntityWithCount<T>> FindAndCountAll(ISpecifications<T> specifications)
        {
            var rows = await FindAllAsync(specifications);

            specifications.IsPagingEnabled = false;

            var count = await GetCountAsync(specifications);

            return new EntityWithCount<T> { Count = count, Rows = rows };
        }

        public virtual async Task<T?> FindByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public virtual async Task<T?> FindOneAync(ISpecifications<T> specifications)
        {
            return await repository.GetOneAsync(specifications);
        }

        public async Task<T?> FindOneAync(Expression<Func<T, bool>> predicate)
        {
            return await repository.GetOneAsync(predicate);
        }

        //create methods
        public virtual async Task<T> CreateAync(T entity)
        {
           var item = await repository.AddAsync(entity);

            await repository.SaveChangesAsync();

            return item;
        }

        //Count methods
        public async Task<int> GetCountAsync()
        {
            return await repository.GetCountAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await repository.GetCountAsync(specifications);
        }

        //update methods
        public virtual async Task<T> UpdateAsync(T entity)
        {
            repository.Update(entity);

            await repository.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(int id, T entity)
        {
            var record = await FindByIdAsync(id);

            if (record == null)
                throw new NotFoundException("Reacord not found");

            UpdateEntityProperties(record, entity);

            await repository.SaveChangesAsync();

            return record;
        }

        //delete methods
        public virtual async Task DeleteAsync(int id)
        {
            var record = await FindByIdAsync(id);

            if (record == null)
                throw new NotFoundException("Reacord not found");

            repository.Delete(record);

            await repository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
        
            repository.Delete(entity);

            await repository.SaveChangesAsync();
        }

        private void UpdateEntityProperties(T target, T source)
        {

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(prop => prop.CanWrite && prop.Name != "Id");

            foreach (var prop in properties)
            {
                var sourceValue = prop.GetValue(source);
              
                prop.SetValue(target, sourceValue);
            }
        }
    }
}
