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
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string Zipcode{ get; set; }
        public int availableSeats { get; set; }
        public int totalSeats { get; set; }
        public int reservedSeats { get; set; }
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }
        public int EventOrganisationId { get; set; }
        public virtual EventOrganisation EventOrganisation{ get; set; }

    }
}
