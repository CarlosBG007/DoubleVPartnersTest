using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Entities;
using TicketManagement.Response;

namespace TicketManagement.TicketServices
{
    public interface ITicketService
    {

        Task<ServiceResponse<Ticket>> CreateTicket(Ticket ticket);
        Task<ServiceResponse<List<Ticket>>> DeleteTicket(int ticketId);
        Task<ServiceResponse<Ticket>> UpdateTicket(Ticket ticket);
        Task<ServiceResponse<List<Ticket>>> GetTickets();
        Task<ServiceResponse<Ticket>> GetTicket(int ticketId);
    }
}
