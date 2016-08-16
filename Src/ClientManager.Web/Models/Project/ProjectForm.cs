using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ClientManager.Web.Models
{
    public class ProjectForm : IMapFrom<Entities.Project>
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }  
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]      
        public DateTime? FechaInicio { get; set; }
        [Required]
        [UIHint("ClientId")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
            
        public Entities.Project ToProject()
        {
            var project = Mapper.Map<ProjectForm, Entities.Project>(this);
            return project;
        }

        public static ProjectForm FromProject(Entities.Project project)
        {
            var form = Mapper.Map<Entities.Project, ProjectForm>(project);
            return form;
        }
    }
}
