using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Common.Utility;
using Framework.Data.Helpers;
using ClientManager.Data.Interfaces;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using Framework.Common.Mapping;
using ClientManager.Data;

namespace ClientManager.Service
{
    public class TimeRecordService : ServiceBase, ITimeRecordService
    {
        private readonly IClock _clock;

        public TimeRecordService(ClientManagerUow uow, IClock clock)
        {
            _clock = clock;
            Uow = uow;
        }

        public Task Create(TimeRecord timeRecord)
        {
            timeRecord.CreatedDate = _clock.Now;
            Uow.TimeRecords.Add(timeRecord);
            return Uow.CommitAsync();
        }

        public Task Edit(TimeRecord timeRecord)
        {
            var currentTimeRecord = this.GetById(timeRecord.Id);
            currentTimeRecord.Descripcion = timeRecord.Descripcion;
            currentTimeRecord.Fecha = timeRecord.Fecha;
            currentTimeRecord.HoraDesde = timeRecord.HoraDesde;
            currentTimeRecord.HoraHasta = timeRecord.HoraHasta;
            currentTimeRecord.Pagado = timeRecord.Pagado;
            currentTimeRecord.ProjectId = timeRecord.ProjectId;
            currentTimeRecord.UserId = timeRecord.UserId;

            Uow.TimeRecords.Edit(currentTimeRecord);
            return Uow.CommitAsync();
        }

        public Task Delete(int timeRecordId)
        {
            var timeRecord = GetById(timeRecordId);
            timeRecord.IsDeleted = true;
            Uow.TimeRecords.Edit(timeRecord);

            return Uow.CommitAsync();
        }

        public TimeRecord GetById(int id)
        {
            return Uow.TimeRecords.Get(id);
        }

        public IList<TimeRecordDto> GetAll()
        {
            return Uow.TimeRecords.GetAll().ProjectToList<TimeRecordDto>();
        }

        public List<TimeRecordDto> GetAll(int projectId, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            Expression<Func<TimeRecord, bool>> where = x => (x.ProjectId == projectId);

            var results = Uow.TimeRecords.GetAll(pagingCriteria, where, x => x.User);

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ToList().Select(Mapper.Map<TimeRecord, TimeRecordDto>).ToList();
        }

    }
}
