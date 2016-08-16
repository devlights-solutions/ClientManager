using System;
using System.Threading.Tasks;
using System.Web.Http;
using ClientManager.Service.Dtos;
using ClientManager.Service.Interfaces;
using ClientManager.Web.Models;
using PagedList;

namespace ClientManager.Web.Controllers.Api
{
    //[Authorize(Roles = "Client")]
    public class ClientController : BaseApiController
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        //[Authorize]
        //[OverrideAuthorization]
        public IHttpActionResult GetAll([FromUri]ClientListFiltersModel filters)
        {
            int pageTotal;

            var list = _clientService.GetAll(filters.Criteria, filters.SortBy, filters.SortDirection, filters.Page, filters.PageSize, out pageTotal);

            var pagedList = new StaticPagedList<ClientDto>(list, filters.Page, filters.PageSize, pageTotal);

            return Ok(new
            {
                List = pagedList,
                TotalItems = pageTotal,
                PageCount = pagedList.PageCount
            });
        }

        // POST: api/Client
        public async Task<IHttpActionResult> Post(ClientForm clientForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = clientForm.ToClient();

                await _clientService.Create(client);

                return Created(Request.RequestUri + client.Id.ToString(), ClientDto.FromClient(client));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Client
        public async Task<IHttpActionResult> Put(ClientForm clientForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientExists = _clientService.GetById(clientForm.Id.GetValueOrDefault());
            if (clientExists == null)
            {
                return NotFound();
            }

            try
            {
                var client = clientForm.ToClient();

                await _clientService.Edit(client);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Client
        public async Task<IHttpActionResult> Delete(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            try
            {
                await _clientService.Delete(client.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
