using MediatR;
using PwcWebApiDemo.Domain;
using PwcWebApiDemo.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Application.Clients
{
    public class Details
    {
        public class Query : IRequest<Client>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Client>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Client> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Clients.FindAsync(request.Id);
            }
        }
    }
}
