using PwcWebApiDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwcWebApiDemo.Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Clients.Any()) return;
            var clients = new List<Client>
            {
                new Client
                {
                    Id=Guid.NewGuid(),
                    Name = "IBM",
                    Email="contact@ibm.com",
                    StartFrom= new DateTime(1990,1,2),
                },
                new Client
                {
                    Id=Guid.NewGuid(),
                    Name = "HP",
                    Email="contact@hp.com",
                    StartFrom= new DateTime(1991,2,3),
                },
                new Client
                {
                    Id=Guid.NewGuid(),
                    Name = "SAP",
                    Email="contact@sap.com",
                    StartFrom= new DateTime(1992,3,4),
                },
                new Client
                {
                    Id=Guid.NewGuid(),
                    Name = "Oracle",
                    Email="contact@oracle.com",
                    StartFrom= new DateTime(1993,4,5),
                }
            };

            await context.Clients.AddRangeAsync(clients);
            await context.SaveChangesAsync();
        }
    }
}
