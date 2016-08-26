using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;

namespace ClientManager.Service.Interfaces
{
    public interface ITimeRecordService : IService
    {
        Task Create(TimeRecord timeRecord);
        Task Edit(TimeRecord timeRecord);
        Task Delete(int timeRecordId);
        Task PagarTareas(IEnumerable<int> ids);
        IList<TimeRecordDto> GetAll();
        
        List<TimeRecordDto> GetAll(int? projectId, int? userId, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        TimeRecord GetById(int id);
        
    }
}