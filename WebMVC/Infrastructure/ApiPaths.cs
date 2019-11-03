using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class ApiPaths
    {
        public static class Order
        {
            public static string GetOrder(string baseUri, string orderId)
            {
                return $"{baseUri}/{orderId}";
            }

            //public static string GetOrdersByUser(string baseUri, string userName)
            //{
            //    return $"{baseUri}/userOrders?userName={userName}";
            //}
            public static string GetOrders(string baseUri)
            {
                return baseUri;
            }
            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new";
            }
        }


        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }
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
               int page, int take, int? category, int?  type, string city,string name,string date)
            {
                var filterQs = string.Empty;

                if (category.HasValue || type.HasValue ||(!string.IsNullOrEmpty(name))||
                    (!string.IsNullOrEmpty(city)) ||((!string.IsNullOrEmpty(date))))
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : " ";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : " ";
                    var nameQs = (!string.IsNullOrEmpty(name)&&(!name.Equals("Search By Title"))) ? name : " ";
                    var cityQs = ((!string.IsNullOrEmpty(city) && (!city.Equals("All")))) ? city : " ";

                    var startDateQs = " ";
                    var endDateQs = " ";
                    if (date.Equals("Today"))
                    {
                        var currentDate = DateTime.Today;
                        startDateQs = currentDate.ToString("yyyyMMdd");
                        endDateQs = currentDate.ToString("yyyyMMdd");
                    }
                    else if (date.Equals("Tomorrow"))
                    {
                        var Date = DateTime.Now.AddDays(1);
                        startDateQs = Date.ToString("yyyyMMdd");
                        endDateQs = Date.ToString("yyyyMMdd");
                    }
                    else if (date.Equals("This Week"))
                    {
                        var day = DateTime.Now.DayOfWeek;
                        int days = day - DayOfWeek.Monday;
                        var startDateOfThisWeek = DateTime.Now.AddDays(-days);
                        var endDateOfThisWeek = startDateOfThisWeek.AddDays(6);
                        startDateQs = startDateOfThisWeek.ToString("yyyyMMdd");
                        endDateQs = endDateOfThisWeek.ToString("yyyyMMdd");
                    }
                    else if (date.Equals("This Weekend"))
                    {
                        var day = DateTime.Now.DayOfWeek;
                        int days = day - DayOfWeek.Saturday;
                        var startDateOfThisWeek = DateTime.Now.AddDays(-days);
                        var endDateOfThisWeek = startDateOfThisWeek.AddDays(1);
                        startDateQs = startDateOfThisWeek.ToString("yyyyMMdd");
                        endDateQs = endDateOfThisWeek.ToString("yyyyMMdd");
                    }
                    else if (date.Equals("Next Week"))
                    {
                        var day = DateTime.Now.DayOfWeek;
                        int days = day - DayOfWeek.Monday;
                        var startDateOfThisWeek = DateTime.Now.AddDays(-days);
                        var startDateOfNextWeek = startDateOfThisWeek.AddDays(7);
                        var endDateOfNextWeek = startDateOfNextWeek.AddDays(6);   
                        startDateQs = startDateOfNextWeek.ToString("yyyyMMdd");
                        endDateQs = endDateOfNextWeek.ToString("yyyyMMdd");
                    }
                    else if (date.Equals("This Month"))
                    {
                        var today = DateTime.Today;
                        var startDateOfThisMonth = new DateTime(today.Year, today.Month,1);
                        int days = DateTime.DaysInMonth(startDateOfThisMonth.Year, startDateOfThisMonth.Month);
                        var endDateOfThisMonth = startDateOfThisMonth.AddDays(days-1);
                        startDateQs = today.ToString("yyyyMMdd");
                        endDateQs = endDateOfThisMonth.ToString("yyyyMMdd");
                    }
                    else if(date.Equals("Next Month"))
                    {
                        var today = DateTime.Today;
                        var startDateOfNextMonth = new DateTime(today.Year,today.Month,1).AddMonths(1);
                        int days = DateTime.DaysInMonth(startDateOfNextMonth.Year,startDateOfNextMonth.Month);
                        var endDateOfNextMonth = startDateOfNextMonth.AddDays(days-1);
                        startDateQs = startDateOfNextMonth.ToString("yyyyMMdd");
                        endDateQs = endDateOfNextMonth.ToString("yyyyMMdd");
                    }
                
                    filterQs = $"/type/{typeQs}/category/{categoryQs}/Name/{nameQs}/location/{cityQs}/Date/{startDateQs}/{endDateQs}/";
                }
                return $"{baseUri}Events{filterQs}?pageIndex={page}&pageSize={take}";

            }

            internal static object CreateItem(string baseUri)
            {
                throw new NotImplementedException();
            }
        }
    }

}



