using System.Collections.Generic;

namespace CallCenter.API.Utils.Helpers.Interfaces
{
    public interface IModelMapper
    {
        TDestination MapSingle<TSource, TDestination>(TSource source);
        IList<TDestination> MapList<TSource, TDestination>(IList<TSource> source);
    }
}
