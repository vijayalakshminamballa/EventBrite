using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public PaginationInfo PaginationInfo { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
       
        public IEnumerable<SelectListItem> StartDate
        { get; set; }
        public IEnumerable<SelectListItem> EndDate       
        { get; set; }
        public IEnumerable<EventItem> CatalogEvents { get; set; }
        
        public string CityFilterApplied { get; set; }
        //[DataType(DataType.Date)]
        public string DateFilterApplied { get; set; }
        public DateTime StartDateFilterApplied { get; set; }
        public DateTime EndDateFilterApplied { get; set; }
        public int? CategoryFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }

}
