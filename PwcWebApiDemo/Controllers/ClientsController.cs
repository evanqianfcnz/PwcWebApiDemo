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
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
