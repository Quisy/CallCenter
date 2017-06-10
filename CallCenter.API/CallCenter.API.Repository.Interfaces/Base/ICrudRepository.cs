using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Repository.Interfaces.Base
{
    public interface ICrudRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        TEntity DeleteById(int id);

        Task<TEntity> DeleteByIdAsync(int id);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        int Save(DbContext context);

        Task<int> SaveAsync(DbContext context);

        IList<TEntity> GetAll();

        Task<IList<TEntity>> GetAllAsync();

        TEntity GetById(long id);

        Task<TEntity> GetByIdAsync(long id);
    }
}
