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
    public class TimeRecordDto : IMapFrom<TimeRecord>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? HoraDesde { get; set; }
        public DateTime? HoraHasta { get; set; }
        public bool Pagado { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; } 
        public string UserEmail { get; set; }


        public TimeRecord ToTimeRecord()
        {
            var entity = Mapper.Map<TimeRecordDto, TimeRecord>(this);
            return entity;
        }

        public static TimeRecordDto FromTimeRecord(TimeRecord entity)
        {
            var form = Mapper.Map<TimeRecord, TimeRecordDto>(entity);
            return form;
        }
    }
}
