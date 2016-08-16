using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClientManager.Entities;
using Framework.Common.Mapping;

namespace ClientManager.Service.Dtos
{
    public class DemoDto : IMapFrom<Demo>, IHaveCustomMappings
    {
        public int Id { get; set; }

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
