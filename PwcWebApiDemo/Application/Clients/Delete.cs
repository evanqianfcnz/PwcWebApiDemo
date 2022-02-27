using AutoMapper;
using MediatR;
using PwcWebApiDemo.Domain;
using PwcWebApiDemo.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Application.Clients
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(                DataContext context                )
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients.FindAsync(request.Id);

                if (client == null) throw new Exception("id not found");

                _context.Remove(client);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
