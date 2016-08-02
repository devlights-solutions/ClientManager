using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Entities;
using Framework.Common.Mapping;

namespace BaseProject.Service.Dtos
{
    public class DemoDto : IMapFrom<Demo>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public Demo ToDemo()
        {
            var entity = Mapper.Map<DemoDto, Demo>(this);
            return entity;
        }

        public static DemoDto FromDemo(Demo entity)
        {
            var form = Mapper.Map<Demo, DemoDto>(entity);
            return form;
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Demo, DemoDto>().ForMember();

        }
    }
}
