using MediatR;
using PwcWebApiDemo.Domain;
using PwcWebApiDemo.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Application.Clients
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Client Client { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients.FindAsync(request.Client.Id);

                client.Name = request.Client.Name ?? client.Name;

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
