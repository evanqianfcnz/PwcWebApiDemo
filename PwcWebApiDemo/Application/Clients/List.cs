using MediatR;
using Microsoft.EntityFrameworkCore;
using PwcWebApiDemo.Domain;
using PwcWebApiDemo.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Application.Clients
{
    public class List
    {
        public class Query : IRequest<List<Client>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Client>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context= context;
            }

            public async Task<List<Client>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Clients.ToListAsync();
            }
        }
    }
}
