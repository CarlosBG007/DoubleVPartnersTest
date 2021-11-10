using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Entities;
using TicketManagement.Response;
using TicketManagement.TicketServices;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Ticket>>>> GetTickets()
        {
            return Ok(await _ticketService.GetTickets());
        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int ticketId)
        {
            var response = await _ticketService.GetTicket(ticketId);
            if (response.ResponseData == null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Ticket>>> AddTicket(Ticket ticket)
        {
            return Created("default", await _ticketService.CreateTicket(ticket));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Ticket>>> UpdateTicket(Ticket ticket)
        {
            var response = await _ticketService.UpdateTicket(ticket);
            if (response.ResponseData == null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult<ServiceResponse<List<Ticket>>>> DeleteTicket(int ticketId)
        {
            var response = await _ticketService.DeleteTicket(ticketId);
            if (response.ResponseData == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}
