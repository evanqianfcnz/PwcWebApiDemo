using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PwcWebApiDemo.Application.Clients;
using PwcWebApiDemo.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Controllers
{ 
    public class ClientsController : BaseApiController
    {
        public ClientsController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            try
            {
                return await Mediator.Send(new List.Query());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            try
            {
                var client = await Mediator.Send(new Details.Query { Id = id });
                if (client == null) return NotFound();
                return client;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            try
            {
                await Mediator.Send(new Create.Command { Client = client });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditClient(Guid id, Client client)
        {
            client.Id = id;
            try
            {
                await Mediator.Send(new Edit.Command { Client = client });
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                await Mediator.Send(new Delete.Command { Id = id});
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
