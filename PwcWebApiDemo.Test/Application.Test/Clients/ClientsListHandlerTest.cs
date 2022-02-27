using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PwcWebApiDemo.Application.Clients;
using PwcWebApiDemo.Persistence;
using Moq;
using Microsoft.EntityFrameworkCore;
using PwcWebApiDemo.Domain;
using System.Threading;
using AutoMapper;
using PwcWebApiDemo.Application.Common;

namespace PwcWebApiDemo.Test.Application.Test.Clients
{
    [TestFixture]
    public class ClientsListHandlerTest
    {
        private DataContext _context;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

            var context = new DataContext(options);
            var clients = new List<Client>
            {
                new Client
                {
                    Id= new Guid("1b366d99-dfad-40ea-bda5-d445dc2f5f77"),
                    Name = "IBM",
                    Email="contact@ibm.com",
                    StartFrom= new DateTime(1990,1,2),
                },
                new Client
                {
                    Id=new Guid("69443c49-1a12-4f69-a1eb-936b4ba3418a"),
                    Name = "HP",
                    Email="contact@hp.com",
                    StartFrom= new DateTime(1991,2,3),
                },
                new Client
                {
                    Id=new Guid("cce96d9e-95c2-4ffb-8b21-d938fa46e36c"),
                    Name = "SAP",
                    Email="contact@sap.com",
                    StartFrom= new DateTime(1992,3,4),
                },
                new Client
                {
                    Id=new Guid("895c6a78-020b-4c08-a47e-39b86d5e4a1a"),
                    Name = "Oracle",
                    Email="contact@oracle.com",
                    StartFrom= new DateTime(1993,4,5),
                }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
            _context = context;

            _mapper = new MapperConfiguration(c => c.AddProfile<MappingProfiles>()).CreateMapper();
        }

        [Test]
        public async Task TestListHandler()
        {
            //Arrange
            var request = new PwcWebApiDemo.Application.Clients.List.Query();
            var handler = new PwcWebApiDemo.Application.Clients.List.Handler(_context);

            //Act
            var clients = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsNotEmpty(clients);
            Assert.AreEqual(clients.Count, 4);
        }
        [Test]
        public async Task TestDetailHandler()
        {
            //Arrange
            var request = new Details.Query();
            request.Id = new Guid("1b366d99-dfad-40ea-bda5-d445dc2f5f77");
            var handler = new Details.Handler(_context);

            //Act
            var client = await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsNotNull(client);
            Assert.AreEqual(client.Name, "IBM");
        }
        [Test]
        public async Task TestCreateHandler()
        {
            //Arrange
            var request = new Create.Command();
            request.Client = new Client
            {
                Id = new Guid("2e4cc2bd-fa20-4b46-99d7-4106c6ead3f3"),
                Name = "Cisco",
                Email = "contact@cisco.com",
                StartFrom = new DateTime(1993, 1, 2),
            };
            var handler = new Create.Handler(_context);

            //Act
            await handler.Handle(request, CancellationToken.None);
            var newClient=_context.Clients.Find(request.Client.Id);

            //Assert
            Assert.IsNotNull(newClient);
            Assert.AreEqual(newClient.Name, request.Client.Name);
            Assert.AreEqual(newClient.Email, request.Client.Email);
            Assert.AreEqual(newClient.StartFrom, request.Client.StartFrom);
        }

        [Test]
        public async Task TestEditHandler()
        {
            //Arrange
            var request = new Edit.Command();
            request.Client = new Client
            {
                Id = new Guid("69443c49-1a12-4f69-a1eb-936b4ba3418a"),
                Name = "Hewlett-Packard",
                Email = "contact_new@hp.co.nz",
                StartFrom = new DateTime(2001, 2, 3),
            };

            
            var handler = new Edit.Handler(_context, _mapper);

            //Act
            await handler.Handle(request, CancellationToken.None);
            var updatedClient = _context.Clients.Find(request.Client.Id);

            //Assert
            Assert.IsNotNull(updatedClient);
            Assert.AreEqual(updatedClient.Name, request.Client.Name);
            Assert.AreEqual(updatedClient.Email, request.Client.Email);
            Assert.AreEqual(updatedClient.StartFrom, request.Client.StartFrom);
        }

        [Test]
        public async Task TestDeleteHandler()
        {
            //Arrange
            var request = new Delete.Command();
            request.Id = new Guid("cce96d9e-95c2-4ffb-8b21-d938fa46e36c");

            var handler = new Delete.Handler(_context);

            //Act
            await handler.Handle(request, CancellationToken.None);
            var client = _context.Clients.Find(request.Id);

            //Assert
            Assert.IsNull(client);
        }

    }
}
