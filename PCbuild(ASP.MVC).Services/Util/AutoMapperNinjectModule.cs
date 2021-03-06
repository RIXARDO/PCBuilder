﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using AutoMapper;

namespace PCbuilder_ASP.MVC_.Services.Util
{
    public class AutoMapperNinjectModule: NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            //Bind<MapperConfiguration>()
            //    .ToConstant(mapperConfiguration).InSingletonScope();

            //Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(AutoMapperServicesProfile));
            });

            return config;
        }
    }
}
