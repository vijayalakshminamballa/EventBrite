using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClient _client;
        private readonly string _baseUri;
        public CatalogService(IConfiguration config,
            IHttpClient client)
        {
            _baseUri = $"{config["CatalogUrl"]}/api/EventCatalog/";
            _client = client;
        }



        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categoryUri = ApiPaths.Catalog.GetAllCategories(_baseUri);
            var dataString = await _client.GetStringAsync(categoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text = "All",
                    Selected = true
                }
            };
            var categories = JArray.Parse(dataString);
            foreach (var category in categories)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = category.Value<string>("id"),
                        Text = category.Value<string>("category")
                    }
                 );
            }
            return items;
        }
        public async Task<IEnumerable<SelectListItem>> GetLocationsAsync()
        {
            var locationUri = ApiPaths.Catalog.GetAllLocations(_baseUri);
            var datastring = await _client.GetStringAsync(locationUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text = "All",
                    Selected = true
                }
            };
            var locations = JArray.Parse(datastring);
            foreach (var location in locations)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = location.ToString(),
                        Text = location.ToString(),
                    }
                 );
            }
            return items;
        }
         public async Task<EventCatalog> GetEventItemsAsync(int page, int size, int? category, int? type,
            string city,string startDate,string endDate)
         {
            var catalogItemsUri = ApiPaths.Catalog.GetAllEventItems(_baseUri,
                    page, size, category, type,city,startDate,endDate);
            var dataString = await _client.GetStringAsync(catalogItemsUri);
            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);
            return response;

         }
        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllTypes(_baseUri);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text = "All",
                    Selected = true
                }
            };
            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    }
                 );
            }
            return items;
        }
    }
}
