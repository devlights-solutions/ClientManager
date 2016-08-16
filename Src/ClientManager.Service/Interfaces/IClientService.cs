using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientManager.Entities;
using ClientManager.Service.Dtos;
using Framework.Common.Mapping;

namespace ClientManager.Service.Interfaces
{
    public interface IClientService : IService
    {
        Task Create(Client client);
        Task Edit(Client client);
        Task Delete(int clientId);
        IList<ClientDto> GetAll();

        List<ClientDto> GetAll(string criteria, string sortBy, string sortDirection, int pageIndex, int pageSize,
            out int pageTotal);
        Client GetById(int id);
    }
}