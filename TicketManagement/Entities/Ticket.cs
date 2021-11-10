using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Status { get; set; }
    }
}
