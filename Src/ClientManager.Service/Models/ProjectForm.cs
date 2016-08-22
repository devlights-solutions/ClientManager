using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;
using System.ComponentModel.DataAnnotations;
using Framework.Common.Validation;

namespace ClientManager.Service.Models
{
    public class ProjectForm : IMapFrom<Entities.Project>
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }
        public decimal? CostoTotal { get; set; }
        public bool Cuotas { get; set; }
        [Display(Name = "Cantidad de Cuotas")]
        [RequiredIf("Cuotas", true)]
        [Range(1, 24, ErrorMessage = "La cantidad de cuotas debe ser un valor entre 1 y 24")]
        public int? CantidadCuotas { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio { get; set; }
        [DataType(DataType.Date)]
        [RequiredIf("Cuotas", true)]
        [Display(Name = "Fecha Pago")]
        public DateTime? FechaPago { get; set; }
        [Required]
        [UIHint("ClientId")]
        [Display(Name = "Cliente", Prompt = "Seleccionar Cliente")]
        public int? ClientId { get; set; }
            
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
