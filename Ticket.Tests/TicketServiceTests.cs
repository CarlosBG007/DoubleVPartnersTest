using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Data;
using TicketManagement.Response;
using TicketManagement.TicketServices;
using Microsoft.EntityFrameworkCore;

namespace Ticket.Tests
{
    public class TicketServiceTests
    {

        private TicketService _ticketService;

        [SetUp]
        public void Setup()
        {
            _ticketService = new TicketService(new DataContext(new DbContextOptions<DataContext>()));
        }

        [Test]
        public async Task CreateTicket_UserNameNotEmtpy_ReturnsCreatedTicket()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                UserName = "jpelaez",
                Status = true
            };
            var response = await _ticketService.CreateTicket(ticket);
            Assert.That(response, Is.TypeOf<ServiceResponse<TicketManagement.Entities.Ticket>>());
        }

        [Test]
        public async Task CreateTicket_UserNameEmtpy_ReturnsNull()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                Status = true
            };
            var response = await _ticketService.CreateTicket(ticket);
            Assert.That(response.ResponseData, Is.EqualTo(null));
        }

        [Test]
        public async Task DeleteTicket_TicketWithIdExists_ReturnsListOfTickets()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                UserName = "cbarragan",
                Status = false
            };
            await _ticketService.CreateTicket(ticket);


            var ticket2 = new TicketManagement.Entities.Ticket()
            {
                UserName = "alopez",
                Status = true,
            };
            await _ticketService.CreateTicket(ticket2);

            var response = await _ticketService.DeleteTicket(1);
            Assert.That(response, Is.TypeOf<ServiceResponse<List<TicketManagement.Entities.Ticket>>>());
        }

        [Test]
        public async Task DeleteTicket_TicketWithIdDoesNotExists_ReturnsNotFoundMessage()
        {
            var response = await _ticketService.DeleteTicket(100);
            Assert.That(response.Message.ToLower(), Is.EqualTo("ticket with id 100 was not found"));
        }

        [Test]
        public async Task GetTicket_TicketWithIdExists_ReturnsTicket()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                UserName = "kvargas",
                Status = true,
            };
            var createdTicket = await _ticketService.CreateTicket(ticket);
            var response = await _ticketService.GetTicket(createdTicket.ResponseData.Id);
            Assert.That(response, Is.TypeOf<ServiceResponse<TicketManagement.Entities.Ticket>>());
        }

        [Test]
        public async Task GetTicket_TicketWithIdDoesNotExists_ReturnsNotFoundMessage()
        {
            var response = await _ticketService.GetTicket(200);
            Assert.That(response.Message.ToLower(), Is.EqualTo("ticket with id 200 was not found"));
        }

        [Test]
        public async Task GetTickets_ReturnsAllTickets()
        {
            var response = await _ticketService.GetTickets();
            Assert.That(response, Is.TypeOf<ServiceResponse<List<TicketManagement.Entities.Ticket>>>());
        }

        [Test]
        public async Task UpdateTicket_TicketExists_ReturnsUpdatedTicket()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                UserName = "mrodriguez",
                Status = true,
            };
            var createdTicket = await _ticketService.CreateTicket(ticket);

            var newTicketData = new TicketManagement.Entities.Ticket()
            {
                Id = createdTicket.ResponseData.Id,
                UserName = "oparrales",
                Status = false,
            };
            var response = await _ticketService.UpdateTicket(newTicketData);
            Assert.That(response, Is.TypeOf<ServiceResponse<TicketManagement.Entities.Ticket>>());
        }

        [Test]
        public async Task UpdateTicket_TicketDoesNotExists_ReturnsNotFoundMessage()
        {
            var ticket = new TicketManagement.Entities.Ticket()
            {
                Id = 1000,
                UserName = "jgomezo",
                Status = true,
            };
            var response = await _ticketService.UpdateTicket(ticket);
            Assert.That(response.Message.ToLower(), Is.EqualTo("ticket with id 1000 was not found"));
        }
    }
}