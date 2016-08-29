using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Framework.Common.Validation;

namespace ClientManager.Web.Models
{
    public class TimeRecordForm : IMapFrom<Entities.TimeRecord>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [UIHint("ProjectId")]
        [Display(Name = "Project", Prompt = "Seleccionar proyecto")]
        public int? ProjectId { get; set; }

        [Required]
        [UIHint("UserId")]
        [Display(Name = "User", Prompt = "Seleccionar usuario")]
        public int? UserId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Hora Desde")]
        [UIHint("Time")]
        public DateTime? HoraDesde { get; set; }

        [Display(Name = "Hora Hasta")]
        [UIHint("Time")]
        [IsDateAfter("HoraDesde", true, ErrorMessage = "La hora de inicio debe ser menor a la hora de fin")]
        public DateTime? HoraHasta { get; set; }

        [Required]        
        public bool Pagado { get; set; }                
                    
        public Entities.TimeRecord ToTimeRecord()
        {
            var timeRecord = Mapper.Map<TimeRecordForm, Entities.TimeRecord>(this);
            return timeRecord;
        }

        public static TimeRecordForm FromTimeRecord(Entities.TimeRecord timeRecord)
        {
            var form = Mapper.Map<Entities.TimeRecord, TimeRecordForm>(timeRecord);
            return form;
        }
    }
}
