using Kanbersky.Customer.Core.Entity;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.Context;
using Kanbersky.Customer.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity, new()
    {
        #region fields

        private readonly KanberContext _context;
        private DbSet<TEntity> _entities;

        #endregion

        #region ctor

        public GenericRepository(KanberContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        #endregion

        #region methods

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> conditions)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(conditions);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            TEntity exists = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (exists != null)
            {
                _context.Entry(exists).CurrentValues.SetValues(entity);
            }
            return exists;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ?
                await _context.Set<TEntity>().ToListAsync() :
                await _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public IEnumerable<TEntity> GetQueryable()
        {
            return _entities.AsEnumerable();
        }

        #endregion
    }
}
