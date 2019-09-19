using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AvailableSeats{get;set;}
        public int ReservedSeats { get; set; }
        public int TotalSeats { get; set; }
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
