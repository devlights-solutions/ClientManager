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
    public class ClientDto : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Responsable { get; set; }
        public string Cuit { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }

        public Client ToClient()
        {
            var entity = Mapper.Map<ClientDto, Client>(this);
            return entity;
        }

        public static ClientDto FromClient(Client entity)
        {
            var form = Mapper.Map<Client, ClientDto>(entity);
            return form;
        }
    }
}
