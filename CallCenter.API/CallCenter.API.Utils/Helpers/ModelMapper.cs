using System.Collections.Generic;
using AutoMapper;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Utils.Helpers
{
    public class ModelMapper : IModelMapper
    {
        public TDestination MapSingle<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public IList<TDestination> MapList<TSource, TDestination>(IList<TSource> source)
        {
            return Mapper.Map<IList<TDestination>>(source);
        }
    }
}
