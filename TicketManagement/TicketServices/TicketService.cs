using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Data;
using TicketManagement.Entities;
using TicketManagement.Response;

namespace TicketManagement.TicketServices
{
    public class TicketService : ITicketService
    {
        private DataContext _context;

        public TicketService(DataContext context)
        {
            _context = context;
        }

        private async Task<int> GetNextId()
        {
            return await _context.Tickets.Select(t => t.Id).DefaultIfEmpty().MaxAsync() + 1;
        }

        public async Task<ServiceResponse<Ticket>> CreateTicket(Ticket ticket)
        {
            var response = new ServiceResponse<Ticket>();
            try
            {
                if (!string.IsNullOrEmpty(ticket.UserName))
                {
                    var newTicket = new Ticket()
                    {
                        Id = await GetNextId(),
                        UserName = ticket.UserName,
                        Status = ticket.Status,
                        CreatedDate = DateTime.Now
                    };
                    _context.Tickets.Add(newTicket);
                    await _context.SaveChangesAsync();
                    response.ResponseData = newTicket;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }

        public async Task<ServiceResponse<List<Ticket>>> DeleteTicket(int ticketId)
        {
            var response = new ServiceResponse<List<Ticket>>();
            try
            {
                var ticketToDelete = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
                if (ticketToDelete != null)
                {
                    _context.Tickets.Remove(ticketToDelete);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = string.Format("Ticket with id {0} was not found", ticketId);
                }
                response.ResponseData = await _context.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Ticket>> GetTicket(int ticketId)
        {
            var response = new ServiceResponse<Ticket>();
            try
            {
                var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
                response.ResponseData = existingTicket;
                if (existingTicket == null)
                {
                    response.Success = false;
                    response.Message = string.Format("Ticket with id {0} was not found", ticketId);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            var response = new ServiceResponse<List<Ticket>>()
            {
                Success = true,
                ResponseData = tickets
            };
            return response;
        }

        public async Task<ServiceResponse<Ticket>> UpdateTicket(Ticket ticket)
        {
            var response = new ServiceResponse<Ticket>();
            try
            {
                var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
                if (existingTicket != null)
                {
                    existingTicket.UserName = ticket.UserName;
                    existingTicket.Status = ticket.Status;
                    existingTicket.UpdatedDate = DateTime.Now;

                    _context.Entry(existingTicket).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = String.Format("Ticket with id {0} was not found", ticket.Id);
                }
                response.ResponseData = existingTicket;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
