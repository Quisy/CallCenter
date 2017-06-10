using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CallCenter.API.Repository.Interfaces.Base
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> QueryAll(DbContext context);

        Task<IQueryable<TEntity>> QueryAllAsync(DbContext context);

        IQueryable<TEntity> QueryBy(DbContext context, Expression<Func<TEntity, bool>> predicate);

        Task<IQueryable<TEntity>> QueryByAsync(DbContext context, Expression<Func<TEntity, bool>> predicate);
    }
}
