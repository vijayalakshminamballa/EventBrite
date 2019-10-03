﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public PaginationInfo PaginationInfo { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<EventCatalog> CatalogEvents { get; set; }

        public int? CategoryFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
    }
}
