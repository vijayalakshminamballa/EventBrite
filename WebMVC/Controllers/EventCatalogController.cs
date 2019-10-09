using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly ICatalogService _service;
        public EventCatalogController(ICatalogService service) =>
            _service = service;
          public async Task<IActionResult> Index(
            int? categoryFilterApplied,
            int? typesFilterApplied,
            int? page,
          string cityFilterApplied,
          string DateFilterApplied)
        {
            var itemsOnPage = 10;
            var catalog =
                await _service.GetEventItemsAsync(page ?? 0,
                itemsOnPage, categoryFilterApplied, typesFilterApplied,cityFilterApplied,DateFilterApplied);


            var vm = new EventCatalogIndexViewModel
            {
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnPage,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                CatalogEvents = catalog.Data,
                Categories = await _service.GetCategoriesAsync(),
                Types = await _service.GetTypesAsync(),
                Locations=await _service.GetLocationsAsync(),
                CategoryFilterApplied = categoryFilterApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0,
                CityFilterApplied = cityFilterApplied ?? " ",
                DateFilterApplied = DateFilterApplied??" ",


            };
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            return View(vm);
        }

    }
}