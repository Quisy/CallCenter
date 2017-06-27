using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CallCenter.API.Services.Configuration;

namespace CallCenter.API.Web
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<MapperModelsProfile>();
            });
        }
    }
}