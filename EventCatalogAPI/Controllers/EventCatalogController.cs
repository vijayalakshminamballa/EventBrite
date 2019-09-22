using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
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
            return Ok(events);
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

        [HttpGet]
        [Route("[action]/Name/{Name:minlength(1)}")]
        public async Task<IActionResult> BrowseEventsByName(string Name,
        [FromQuery] int pageSize = 5,
        [FromQuery] int pageIndex = 0)
        {
            var totalEvents = await _context.EventItem
                .Where(e => e.Name.StartsWith(Name))
                .LongCountAsync();
            var events = await _context.EventItem
                .Where(e => e.Name.StartsWith(Name))
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
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
        [Route("[action]/city/{CityPrefix:minlength(1)}")]
        public async Task<IActionResult> CityDropDown(string CityPrefix,
         [FromQuery] int pageSize = 6,
         [FromQuery] int pageIndex = 0)
        {
            var totalEvents = await _context.EventItem
           .Where(e => e.Name.StartsWith(CityPrefix))
           .LongCountAsync();
            var cities = await _context.EventItem
                .Where(e => e.City.StartsWith(CityPrefix))
                .OrderBy(e => e.City)
                .Select(e => e.City).Distinct()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            return Ok(cities);
        }
    }
}










