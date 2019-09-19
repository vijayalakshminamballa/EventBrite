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
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string PictureUrl { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State{ get; set; }
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; } 
        public int EventCategoryId { get; set; } 
        public virtual EventCategory EventCategory{ get; set; }


    }
}
