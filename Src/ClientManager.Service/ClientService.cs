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
    public class ClientService : ServiceBase, IClientService
    {
        private readonly IClock _clock;

        public ClientService(ClientManagerUow uow, IClock clock)
        {
            _clock = clock;
            Uow = uow;
        }

        public Task Create(Client client)
        {
            client.CreatedDate = _clock.Now;
            Uow.Clients.Add(client);
            return Uow.CommitAsync();
        }

        public Task Edit(Client client)
        {
            var currentClient = this.GetById(client.Id);
            currentClient.RazonSocial = client.RazonSocial;
            currentClient.Responsable = client.Responsable;
            currentClient.Cuit = client.Cuit;
            currentClient.Dni = client.Dni;
            currentClient.Telefono = client.Telefono;
            currentClient.Direccion = client.Direccion;
            currentClient.Celular = client.Celular;

            Uow.Clients.Edit(currentClient);
            return Uow.CommitAsync();
        }

        public Task Delete(int clientId)
        {
            var client = GetById(clientId);
            client.IsDeleted = true;
            Uow.Clients.Edit(client);

            return Uow.CommitAsync();
        }

        public Client GetById(int id)
        {
            return Uow.Clients.Get(id);
        }

        public IList<ClientDto> GetAll()
        {
            return Uow.Clients.GetAll().ProjectToList<ClientDto>();
        }

        public List<ClientDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize, out int pageTotal)
        {
            var pagingCriteria = new PagingCriteria();

            pagingCriteria.PageNumber = pageIndex;
            pagingCriteria.PageSize = pageSize;
            pagingCriteria.SortBy = !string.IsNullOrEmpty(sortBy) ? sortBy : DefaultSortBy;
            pagingCriteria.SortDirection = !string.IsNullOrEmpty(sortDirection) ? sortDirection : DefaultSortDirection;

            Expression<Func<Client, bool>> where = x => ((string.IsNullOrEmpty(criteria) || x.RazonSocial.Contains(criteria)));

            var results = Uow.Clients.GetAll(pagingCriteria, where);

            pageTotal = results.PagedMetadata.TotalItemCount;

            return results.Entities.ProjectToList<ClientDto>();
        }

    }
}
