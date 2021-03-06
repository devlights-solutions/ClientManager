﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Framework.Common.Mapping;

namespace ClientManager.Web
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            var typesReference = Assembly
                           .GetExecutingAssembly()
                           .GetReferencedAssemblies()
                           .Where(x => x.Name.StartsWith("ClientManager"))
                           .Select(x => Assembly.Load(x))
                           .SelectMany(x => x.GetTypes()).ToArray();

            var typesCurrentAssembly = Assembly.GetExecutingAssembly().GetExportedTypes();

            var types = typesCurrentAssembly.Concat(typesReference).ToArray();

            Mapper.Initialize(cfg =>
            {
                LoadStandardMappings(types, cfg);

                LoadCustomMappings(types, cfg);

                LoadProfileMapping(cfg);

                ConfigBuildedInMapping(cfg);
            });
            
        }

        private static void LoadProfileMapping(IMapperConfigurationExpression cfg)
        {
            //Mapper.AddProfile<CategoryMappingProfile>();
        }

        private static void ConfigBuildedInMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DateTime, TimeSpan>().ConvertUsing(x => x.TimeOfDay);
            cfg.CreateMap<TimeSpan, DateTime>().ConvertUsing(x => new DateTime(x.Ticks));
            cfg.CreateMap<DateTime?, TimeSpan?>().ConvertUsing(x => x.HasValue ? x.Value.TimeOfDay : (TimeSpan?)null);
            cfg.CreateMap<TimeSpan?, DateTime?>().ConvertUsing(x => x.HasValue ?  new DateTime(x.Value.Ticks) : (DateTime?)null);
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(cfg);
            }
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                cfg.CreateMap(map.Source, map.Destination);
                cfg.CreateMap(map.Destination, map.Source);
            }
        }
    }
}