using Microsoft.EntityFrameworkCore;
using PwcWebApiDemo.Domain;

namespace PwcWebApiDemo.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions options): base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
    }
}
