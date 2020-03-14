using Kanbersky.Customer.Core.Entity;
using Kanbersky.Customer.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity, IEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<List<T>> GetList(Expression<Func<T, bool>> expression = null);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();

        IEnumerable<T> GetQueryable();
    }
}
