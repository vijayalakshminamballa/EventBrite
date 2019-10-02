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
                return $"{baseUri}EventTypes";
            }
            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}EventCategories";
            }
            public static string GetAllEventItems(string baseUri,
               int page, int take, int? category, int? type,string city,string startDate,string endDate)
            {
                var filterQs = string.Empty;

                if (category.HasValue || type.HasValue)
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : "null";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/brand/{categoryQs}";
                }

                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";

            }


        }

    }

    }



