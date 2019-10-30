using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class EventItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string PictureUrl { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int EventCapacity { get; set; }
        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public int EventCategoryId { get; set; }
        public string EventCategory { get; set; }

    }
}
