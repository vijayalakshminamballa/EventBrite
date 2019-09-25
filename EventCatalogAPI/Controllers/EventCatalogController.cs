using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
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
          [FromQuery] int pageSize = 5)
        {
            var eventsCount = await
                _context.EventItem.LongCountAsync();
            var events = await _context.EventItem
                .OrderBy(e => e.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize=pageSize,
                Count=eventsCount,
                Data=events
            };
            return Ok(model);
        }

        [HttpGet]
        [Route("[action]/type/{eventTypeId}/category/{eventCategoryId}/location/{city}/Date/{startDatestr}/{endDatestr}")]
        public async Task<IActionResult> BrowseEvents(
        int? eventTypeId,
        int? eventCategoryId,
        string city,
        string startDateStr,
        string endDateStr,
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
            if (!String.IsNullOrEmpty(startDateStr) && !String.IsNullOrEmpty(endDateStr))
            {

                DateTime StartDate = DateTime.ParseExact(startDateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime EndDate = DateTime.ParseExact(endDateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
                root = root.Where(e => e.Date >= StartDate && e.Date <= EndDate);
            }

            var eventsCount = await
                root.LongCountAsync();

            var events = await root
                .OrderBy(e => e.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
           events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = eventsCount,
                Data = events
            };
            return Ok(model);

        }

        [HttpGet]
        [Route("[action]/Name/{Name:minlength(1)}")]
        public async Task<IActionResult> BrowseEventsByName(string Name,
        [FromQuery] int pageSize = 5,
        [FromQuery] int pageIndex = 0)
        {
            var eventsCount = await _context.EventItem
                .Where(e => e.Name.StartsWith(Name))
                .LongCountAsync();
            var events = await _context.EventItem
                .Where(e => e.Name.StartsWith(Name))
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedEventsViewModel<EventItem>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = eventsCount,
                Data = events
            };
            return Ok(model);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateEvent([FromBody]EventItem Event)
        {
            var item = new EventItem
            {
                EventCategoryId = Event.EventCategoryId,
                EventTypeId = Event.EventTypeId,
                Description = Event.Description,
                Name = Event.Name,
                Price = Event.Price,
                Date=Event.Date,
                AddressLine=Event.AddressLine,
                City=Event.City,
                State=Event.State,
                PictureUrl = Event.PictureUrl,
            };
            _context.EventItem.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventById), new { id = item.Id }, item);
        }

        [HttpGet]
        [Route("[action]/Id/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var item = await _context.EventItem
             .FirstOrDefaultAsync(e => e.Id == id);
            return Ok(item);
        }

        [HttpDelete]
        [Route("[action]/Id/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var item = await _context.EventItem
           .FirstOrDefaultAsync(e => e.Id == id);
            _context.EventItem.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(item);
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
            var events = await _context.EventType.ToListAsync();
            return Ok(events);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategory()
        {
            var events = await _context.EventCategories.ToListAsync();
            return Ok(events);
        }
        
        [HttpGet]
        [Route("[action]/city/{CityPrefix:minlength(1)}")]
        public async Task<IActionResult> CityDropDown(string CityPrefix)

        {
           var totalEvents = await _context.EventItem
           .Where(e => e.Name.StartsWith(CityPrefix))
           .LongCountAsync();
            var cities = await _context.EventItem
                .Where(e => e.City.StartsWith(CityPrefix))
                .OrderBy(e => e.City)
                .Select(e => e.City).Distinct()
                .ToListAsync();
            return Ok(cities);
        }
    }
}










