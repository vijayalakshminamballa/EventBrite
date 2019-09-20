using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCatalogController : ControllerBase
    {
        private readonly CatalogEventContext _context;
        private readonly IConfiguration _config;
        public EventCatalogController(CatalogEventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events(
          [FromQuery] int pageIndex = 0,
          [FromQuery] int pageSize = 6)
        {
            var eventsCount = await
                _context.EventItem.LongCountAsync();
            var events = await _context.EventItem
                .OrderBy(e => e.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            events = ChangePictureUrl(events);

            return Ok(events);
        }

        [HttpGet]
        [Route("[action]/type/{EventTypeId}/Category/{EventCategoryId}/location/{City}/Date/{Date}")]
        public async Task<IActionResult> Events(
        int? eventTypeId,
        int? eventCategoryId,
        string city,
        DateTime? StartDate,
        DateTime?EndDate,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 5)
        {
            var root = (IQueryable<EventItem>)_context.EventItem;
            if (eventTypeId.HasValue)
            {
                root =
                    root.Where(e => e.EventTypeId == eventTypeId);
            }
            if (eventCategoryId.HasValue)
            {
                root =
                    root.Where(e => e.EventCategoryId == eventCategoryId);
            }
            
            if (!String.IsNullOrEmpty(city))
            {
                root =
                    root.Where(e => e.City == city);
            }
            if(StartDate.HasValue && EndDate.HasValue)
            {

                root = root.Where(e => e.Date > StartDate && e.Date < EndDate);

            }

            var itemsCount = await
                root.LongCountAsync();

            var events = await root
                .OrderBy(e => e.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            events = ChangePictureUrl(events);

            return Ok(events);
        }

        private List<EventItem> ChangePictureUrl(List<EventItem> events)
        {
            events.ForEach(
                c => c.PictureUrl = c.PictureUrl
                        .Replace("http://externalcatalogbaseurltobereplaced",
                        _config["ExternalCatalogBaseUrl"]));
            return events;

        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventType()
        {
            var items = await _context.EventType.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategory()
        {
            var items = await _context.EventCategories.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]/city/{name:minlength(1)}")]
        public async Task<IActionResult> Events(string City,
         [FromQuery] int pageSize = 6,
         [FromQuery] int pageIndex = 0)
        {
            var totalEvents = await _context.EventItem
                .Where(e => e.City.StartsWith(City))
                .LongCountAsync();
            var events = await _context.EventItem
                .Where(e => e.City.StartsWith(City))
                .OrderBy(c => c.City)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            events = ChangePictureUrl(events);

           
            return Ok(events);
        }
    }
}






        
    

