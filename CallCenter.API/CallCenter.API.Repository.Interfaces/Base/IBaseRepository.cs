using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CallCenter.API.Repository.Interfaces.Base
{
    public interface IBaseRepository<TModel>
        where TModel : class
    {
        IQueryable<TModel> QueryAll(DbContext context);

        Task<IQueryable<TModel>> QueryAllAsync(DbContext context);

        IQueryable<TModel> QueryBy(DbContext context, Expression<Func<TModel, bool>> predicate);

        Task<IQueryable<TModel>> QueryByAsync(DbContext context, Expression<Func<TModel, bool>> predicate);
    }
}
