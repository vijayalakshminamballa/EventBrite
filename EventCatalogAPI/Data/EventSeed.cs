using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventSeed
    {

        public static void Seed(CatalogEventContext context)
        {
            context.Database.Migrate();
            if (!context.EventCategories.Any())
            {
                context.EventCategories.AddRange
                    (GetPreconfiguredEventCategory());
                context.SaveChanges();
            }
            if (!context.EventType.Any())
            {
                context.EventType.AddRange
                    (GetPreconfiguredEventType());
                context.SaveChanges();
            }

            if (!context.EventItem.Any())
            {
                context.EventItem.AddRange
                       (GetPreconfiguredEvent());
                context.SaveChanges();
            }
        }

        private static IEnumerable<EventItem> GetPreconfiguredEvent()
        {
            return new List<EventItem>()
            {
                new EventItem() { EventTypeId= 1, EventCategoryId=1, Description ="International Food Truck Rally presented by the City of Tukwila",Name ="International Food Truck Rally",
Date=DateTime.Parse("12/24/2019"),Price=30,Organizer="WestFieldSouthern Center",AddressLine= "Cross Roads Mall",City="Bellevue" ,State="WA",PictureUrl = "http://externalcatalogtobereplaced/api/pic/1" },

                new EventItem() { EventTypeId= 2,EventCategoryId=1,Description ="Cioppino Noir teaches you the basics of choosing the right seafood",Name ="Palisade Fresh Catch Series",
Date=DateTime.Parse("12/12/2019"),Price=50,Organizer="Palesade Restaurent", AddressLine="Overlake Plaza",City= "Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2"},

                new EventItem() { EventTypeId= 3,EventCategoryId=1,Description ="The Grand Finale of the Famous Christmas Ships Parade on Lake Union!", Name = "Christmas Ships Finale Cruise!",
Date=DateTime.Parse("10/12/2019"),Price=100.5M,Organizer="Washington Wine cruise",AddressLine="South Lake Union",City="Seattle",State="WA",PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/3"},

                new EventItem() { EventTypeId= 2,EventCategoryId=2,Description ="Truly Practical Data Science Training with Real-Life Cases",Name ="Data Science Training",
Date=DateTime.Parse("09/30/2019"),Price=60.8M,Organizer="Altoros", AddressLine="Tamber Creek Estates",City="Bothell",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4"},

                new EventItem() { EventTypeId= 5,EventCategoryId=2,Description ="This Business Analyst seminar provided  for beginners",Name ="Business Analyst Seminar ",
Date=DateTime.Parse("10/25/2019"),Price=54,Organizer="Entirely Technology",AddressLine="Bellevue Collge",City="Bellevue",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5"},

                new EventItem() { EventTypeId= 2,EventCategoryId=2,Description ="This IoT training is a LIVE Instructor led training",Name ="IoT training  ",
Date=DateTime.Parse("12/16/2019"),Price=45,Organizer="TrueVs",AddressLine="Safeco Plaza",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6"},

                new EventItem() { EventTypeId= 2,EventCategoryId=3,Description ="Understanding your role as a product manager",Name ="Product Management Training ",
Date=DateTime.Parse("11/18/2019"),Price=23,Organizer="Mind the Product",AddressLine="Seattle Learning Center",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"},

                new EventItem() { EventTypeId= 5,EventCategoryId=3,Description ="Webinar - Sales and Marketing Growth Strategies for 2019!",Name = "Sales and Marketing",
Date=DateTime.Parse("10/25/2019"),Price=90,Organizer="Local SEO Search",AddressLine="Charlotte",City="Charlotte",State="NC",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new EventItem() { EventTypeId= 4,EventCategoryId=5,Description ="The Faim & Stand Atlantic @ The Vera Project ",Name = "Music Performance ",
Date=DateTime.Parse("12/20/2019"),Price=200,Organizer="The Crocodile",AddressLine="The Lakes",City="Renton",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },

                new EventItem() { EventTypeId= 2,EventCategoryId=5,Description ="Learn to Sing with Vardhani Mellacheruvu",Name = "Carnatic Vocal Music Classes ",
Date=DateTime.Parse("12/26/2019"),Price=10,Organizer="Kailasa of Seattle",AddressLine="LV Temple" ,City="Bellevue",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },

                new EventItem() { EventTypeId= 2,EventCategoryId=4,Description ="Free Soccer Classes for 2-3 Year Olds ",Name = " Soccer Classes",
Date=DateTime.Parse("11/23/2019"),Price=32,Organizer="Scoccer ShotS Seattle",AddressLine="GreenLake Playgroud",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },

                new EventItem() { EventTypeId= 2,EventCategoryId=4,Description ="Join us for a FREE Project Bollywood Pop-up class ",Name = "Pop-up class",
Date=DateTime.Parse("11/29/2019"),Price=73,Organizer="Bolly Works",AddressLine="Trails of Redmond",City="Redmond",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },

                new EventItem() { EventTypeId= 2,EventCategoryId=4,Description ="FREE Yoga with a View at Hotel Sorrento ",Name = "Yoga Classes ",
Date=DateTime.Parse("10/30/2019"),Price=25,Organizer="Hotel Sorrento",AddressLine="Hotel Sorrento" ,City="Seattle",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },

                new EventItem() { EventTypeId= 4,EventCategoryId=5,Description ="The Chainsmokers/5 Seconds of Summer/Lennon Stella: World War Joy Tour", Name = "Chainsmoker tour",
Date=DateTime.Parse("12/31/2019"),Price=70,Organizer="ChainSmokers",AddressLine="Tacoma Dome" ,City="Tacoma",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },

                new EventItem() { EventTypeId= 3,EventCategoryId=1,Description ="Two days of wine and charcuterie pairings at five Bainbridge Island wineries ", Name = "Wine on the Rock: Wine & Charcuterie ",
Date=DateTime.Parse("12/01/2019"),Price=59,Organizer="Winery Alliance", AddressLine="Five Bainbridge Island wineries" ,City="Bainbridge",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

            };
        }

    

    private static IEnumerable<EventType> GetPreconfiguredEventType()
        {
            return new List<EventType>()
            {
                new EventType() {Type = "Festival"},
                new EventType() {Type = "Class"},
                new EventType() {Type = "Party"},
                new EventType() {Type = "Performance"},
                new EventType() {Type = "Seminar"}

            };
    }

    private static IEnumerable<EventCategory> GetPreconfiguredEventCategory()
    {

            return new List<EventCategory>()
            {
                new EventCategory() {Category = "Food and Drink"},
                new EventCategory() {Category = "Science and tech"},
                new EventCategory() {Category = "Business"},
                new EventCategory() {Category = "Sports and Fitness"},
                new EventCategory() {Category = "Music"}
             };
    }

        


    }
}
