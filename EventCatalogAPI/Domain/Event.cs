using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string PictureUrl { get; set; }
        public int availableSeats { get; set; }
        public int totalSeats { get; set; }
        public int reservedSeats { get; set; }
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }
        public int EventLocationId { get; set; }
        public virtual EventLocation EventLocation{ get; set; }

    }
}
