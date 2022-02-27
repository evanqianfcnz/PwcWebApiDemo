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

            return await Mediator.Send(new List.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });

        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            await Mediator.Send(new Create.Command { Client = client });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditClient(Guid id, Client client)
        {
            client.Id = id;
            await Mediator.Send(new Edit.Command { Client = client });
            return Ok();
        }
    }
}
