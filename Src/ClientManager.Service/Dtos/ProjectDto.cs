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
    public class ProjectDto : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string ClientRazonSocial { get; set; }
        

        public Project ToProject()
        {
            var entity = Mapper.Map<ProjectDto, Project>(this);
            return entity;
        }

        public static ProjectDto FromProject(Project entity)
        {
            var form = Mapper.Map<Project, ProjectDto>(entity);
            return form;
        }
    }
}
