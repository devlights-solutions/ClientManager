using System;
using System.Web.Mvc;
using AutoMapper;
using Framework.Common.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ClientManager.Web.Models
{
    public class ClientForm : IMapFrom<Entities.Client>
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public string Responsable { get; set; }
        [Required]
        public string Cuit { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        public string Celular { get; set; }

        public Entities.Client ToClient()
        {
            var client = Mapper.Map<ClientForm, Entities.Client>(this);
            return client;
        }

        public static ClientForm FromClient(Entities.Client client)
        {
            var form = Mapper.Map<Entities.Client, ClientForm>(client);
            return form;
        }
    }
}
