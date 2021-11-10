using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.Response
{
    public class ServiceResponse<T>
    {
        public T ResponseData { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
