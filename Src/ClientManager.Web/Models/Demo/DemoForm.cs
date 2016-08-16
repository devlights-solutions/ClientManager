using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;

namespace ClientManager.Web.Models
{
    public class DemoForm : IMapFrom<Entities.Demo>
    {
        [HiddenInput]
        public int? Id { get; set; }

        public Entities.Demo ToDemo()
        {
            var demo = Mapper.Map<DemoForm, Entities.Demo>(this);
            return demo;
        }

        public static DemoForm FromDemo(Entities.Demo demo)
        {
            var form = Mapper.Map<Entities.Demo, DemoForm>(demo);
            return form;
        }
    }
}
