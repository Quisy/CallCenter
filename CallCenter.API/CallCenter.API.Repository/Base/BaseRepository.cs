using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CallCenter.API.Repository.Interfaces.Base;

namespace CallCenter.API.Repository.Base
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
        where TModel : class
    {
        public virtual IQueryable<TModel> QueryAll(DbContext context)
        {
            return context.Set<TModel>();
        }

        public async Task<IQueryable<TModel>> QueryAllAsync(DbContext context)
        {
            return await Task.Run(() => QueryAll(context));
        }

        public IQueryable<TModel> QueryBy(DbContext context, Expression<Func<TModel, bool>> predicate)
        {
            return context.Set<TModel>().Where(predicate);
        }

        public async Task<IQueryable<TModel>> QueryByAsync(DbContext context, Expression<Func<TModel, bool>> predicate)
        {
            return await Task.Run(() => QueryBy(context, predicate));
        }
    }
}
