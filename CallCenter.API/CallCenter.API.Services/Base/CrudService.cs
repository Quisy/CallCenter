using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Repository.Interfaces.Base;
using CallCenter.API.Services.Interfaces.Base;

namespace CallCenter.API.Services.Base
{
    public abstract class CrudService<TModel, TRepository, TEntity> : ICrudService<TModel>
        where TModel : class
        where TEntity : class
        where TRepository : ICrudRepository<TEntity>
    {
        protected readonly TRepository Repository;

        protected CrudService(TRepository repository)
        {
            Repository = repository;
        }
    }
}
