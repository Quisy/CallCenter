using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CallCenter.API.DomainModel.Base;
using CallCenter.API.Repository.Interfaces.Base;

namespace CallCenter.API.Repository.Base
{
    public abstract class CrudRepository<TContext, TEntity> : BaseRepository<TEntity>, ICrudRepository<TEntity>
        where TEntity : KeyEntity, new()
        where TContext : DbContext, new()
    {
        //protected abstract IQueryable<TEntity> QueryAll(TContext context);

        public virtual TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Set<TEntity>().Add(entity);
                int saveResult = Save(context);
                return saveResult > 0 ? addedEntity : null;
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return await Task.Run(() => Add(entity));
        }

        public virtual TEntity Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Set<TEntity>().Remove(entity);
                int saveResult = Save(context);
                return saveResult > 0 ? deletedEntity : null;
            }
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await Task.Run(() => Delete(entity));
        }

        public virtual TEntity DeleteById(int id)
        {
            using (var context = new TContext())
            {
                var entity = new TEntity() { Id = id };
                context.Set<TEntity>().Attach(entity);
                var deletedEntity = context.Set<TEntity>().Remove(entity);
                int saveResult = Save(context);
                return saveResult > 0 ? deletedEntity : null;
            }
        }

        public virtual async Task<TEntity> DeleteByIdAsync(int id)
        {
            return await Task.Run(() => DeleteById(id));
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                Save(context);
                return GetById(entity.Id);
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => Update(entity));
        }

        public int Save(DbContext context)
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveAsync(DbContext context)
        {
            return await context.SaveChangesAsync();
        }

        public virtual IList<TEntity> GetAll()
        {
            using (var context = new TContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                var list = QueryAll(context).ToList();
                return list;
            }
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public virtual TEntity GetById(long id)
        {
            using (var context = new TContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                return context.Set<TEntity>().Find(id);
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await Task.Run(() => GetById(id));
        }
    }
}
