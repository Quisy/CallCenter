using System.Collections.Generic;
using CallCenter.API.Repository.Interfaces.Base;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Base
{
    public abstract class CrudService<TModel, TRepository, TEntity> : BaseService, ICrudService<TModel>
        where TModel : class
        where TEntity : class
        where TRepository : ICrudRepository<TEntity>
    {
        protected readonly TRepository Repository;
        public readonly IModelMapper ModelMapper;

        protected CrudService(TRepository repository, IModelMapper modelMapper)
        {
            Repository = repository;
            ModelMapper = modelMapper;
        }

        public virtual Result<IList<TModel>> GetAll()
        {
            var result = ModelMapper.MapList<TEntity, TModel>(Repository.GetAll());
            return Result<IList<TModel>>.ErrorWhenNoData(result);
        }

        public virtual Result<TModel> GetById(int id)
        {
            var result = ModelMapper.MapSingle<TEntity, TModel>(Repository.GetById(id));
            return Result<TModel>.ErrorWhenNoData(result);
        }

        public Result<TModel> Add(TModel model)
        {
            var entity = ModelMapper.MapSingle<TModel, TEntity>(model);
            var result = ModelMapper.MapSingle<TEntity, TModel>(Repository.Add(entity));
            return Result<TModel>.ErrorWhenNoData(result);
        }

        public Result<TModel> Remove(TModel model)
        {
            var entity = ModelMapper.MapSingle<TModel, TEntity>(model);
            var result = ModelMapper.MapSingle<TEntity, TModel>(Repository.Delete(entity));
            return Result<TModel>.ErrorWhenNoData(result);
        }

        public Result<TModel> Update(TModel model)
        {
            var entity = ModelMapper.MapSingle<TModel, TEntity>(model);
            var result = ModelMapper.MapSingle<TEntity, TModel>(Repository.Update(entity));
            return Result<TModel>.ErrorWhenNoData(result);
        }
    }
}
