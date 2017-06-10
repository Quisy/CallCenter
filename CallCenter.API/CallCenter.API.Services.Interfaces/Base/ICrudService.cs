using System.Collections;
using System.Collections.Generic;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Base
{
    public interface ICrudService<TModel> : IBaseService
    {
        Result<IList<TModel>> GetAll();

        Result<TModel> GetById(int id);

        Result<TModel> Add(TModel model);

        Result<TModel> Remove(TModel model);

        Result<TModel> Update(TModel model);
    }
}
