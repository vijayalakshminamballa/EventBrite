using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}EventType";
            }
            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}EventCategory";
            }
            public static string GetAllLocations(string baseUri)
            {

                return $"{baseUri}City";
            }
            public static string GetAllEventItems(string baseUri,
               int page, int take, int? category, int?  type, string city, string startDate, string endDate)
            {
                var filterQs = string.Empty;

                if (category.HasValue || type.HasValue||(!string.IsNullOrEmpty(city)) ||
                   ((!string.IsNullOrEmpty(startDate)) && (!string.IsNullOrEmpty(endDate))))
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : " ";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : " ";
                    var cityQs = (!string.IsNullOrEmpty(city)) ? city : " ";
                    var startDateQs = (!string.IsNullOrEmpty(startDate))? startDate: " ";
                    var endDateQs =  (!string.IsNullOrEmpty(endDate))? endDate: " ";

                    filterQs = $"/type/{typeQs}/category/{categoryQs}/location/{cityQs}/Date/{startDateQs}/{endDateQs}/";
                }

                return $"{baseUri}Events{filterQs}?pageIndex={page}&pageSize={take}";

            }


        }

    }

    }



