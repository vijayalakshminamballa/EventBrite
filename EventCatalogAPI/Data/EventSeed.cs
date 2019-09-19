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
            if (!context.Ticket.Any())
            {
                context.Ticket.AddRange
                    (GetPreconfiguredTicket());
                context.SaveChanges();
            }
            if (!context.Events.Any())
            {
                context.Events.AddRange
                       (GetPreconfiguredEvent());
                context.SaveChanges();
            }
        }

        private static IEnumerable<Event> GetPreconfiguredEvent()
        {
            return new List<Event>()
            {
                new Event() { EventTypeId= 1, EventCategoryId=1, Description ="International Food Truck Rally presented by the City of Tukwila",Name ="International Food Truck Rally",
Date=new DateTime(12/24/2019),Organizer="WestFieldSouthern Center",AddressLine= "Cross Roads Mall",City="Bellevue" ,State="WA",PictureUrl = "http://externalcatalogtobereplaced/api/pic/1" },

                new Event() { EventTypeId= 2,EventCategoryId=1,Description ="Cioppino Noir teaches you the basics of choosing the right seafood",Name ="Palisade Fresh Catch Series",
Date=new DateTime(12/12/2019),Organizer="Palesade Restaurent", AddressLine="Overlake Plaza",City= "Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2"},

                new Event() { EventTypeId= 3,EventCategoryId=1,Description ="The Grand Finale of the Famous Christmas Ships Parade on Lake Union!", Name = "Christmas Ships Finale Cruise!",
Date=new DateTime(10/12/2019),Organizer="Washington Wine cruise",AddressLine="South Lake Union",City="Seattle",State="WA",PictureUrl="http://externalcatalogbaseurltobereplaced/api/pic/3"},

                new Event() { EventTypeId= 2,EventCategoryId=2,Description ="Truly Practical Data Science Training with Real-Life Cases",Name ="Data Science Training",
Date=new DateTime(09/30/2019),Organizer="Altoros", AddressLine="Tamber Creek Estates",City="Bothell",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4"},

                new Event() { EventTypeId= 5,EventCategoryId=2,Description ="This Business Analyst seminar provided  for beginners",Name ="Business Analyst Seminar ",
Date=new DateTime(10/25/2019),Organizer="Entirely Technology",AddressLine="Bellevue Collge",City="Bellevue",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5"},

                new Event() { EventTypeId= 2,EventCategoryId=2,Description ="This IoT training is a LIVE Instructor led training",Name ="IoT training  ",
Date=new DateTime(12/16/2019),Organizer="TrueVs",AddressLine="Safeco Plaza",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6"},

                new Event() { EventTypeId= 2,EventCategoryId=3,Description ="Understanding your role as a product manager",Name ="Product Management Training ",
Date=new DateTime(11/18/2019),Organizer="Mind the Product",AddressLine="Seattle Learning Center",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"},

                new Event() { EventTypeId= 5,EventCategoryId=3,Description ="Webinar - Sales and Marketing Growth Strategies for 2019!",Name = "Sales and Marketing",
Date=new DateTime(10/25/2019),Organizer="Local SEO Search",AddressLine="Charlotte",City="Charlotte",State="NC",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new Event() { EventTypeId= 4,EventCategoryId=5,Description ="The Faim & Stand Atlantic @ The Vera Project ",Name = "Music Performance ",
Date=new DateTime(12/20/2019),Organizer="The Crocodile",AddressLine="The Lakes",City="Renton",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },

                new Event() { EventTypeId= 2,EventCategoryId=5,Description ="Learn to Sing with Vardhani Mellacheruvu",Name = "Carnatic Vocal Music Classes ",
Date=new DateTime(12/26/2019),Organizer="Kailasa of Seattle",AddressLine="LV Temple" ,City="Bellevue",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },

                new Event() { EventTypeId= 2,EventCategoryId=4,Description ="Free Soccer Classes for 2-3 Year Olds ",Name = " Soccer Classes",
Date=new DateTime(11/23/2019),Organizer="Scoccer ShotS Seattle",AddressLine="GreenLake Playgroud",City="Seattle",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },

                new Event() { EventTypeId= 2,EventCategoryId=4,Description ="Join us for a FREE Project Bollywood Pop-up class ",Name = "Pop-up class",
Date=new DateTime(11/29/2019),Organizer="Bolly Works",AddressLine="Trails of Redmond",City="Redmond",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },

                new Event() { EventTypeId= 2,EventCategoryId=4,Description ="FREE Yoga with a View at Hotel Sorrento ",Name = "Yoga Classes ",
Date=new DateTime(10/30/2019),Organizer="Hotel Sorrento",AddressLine="Hotel Sorrento" ,City="Seattle",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },

                new Event() { EventTypeId= 4,EventCategoryId=5,Description ="The Chainsmokers/5 Seconds of Summer/Lennon Stella: World War Joy Tour", Name = "Chainsmoker tour",
Date=new DateTime(12/31/2019),Organizer="ChainSmokers",AddressLine="Tacoma Dome" ,City="Tacoma",State="WA",PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },

                new Event() { EventTypeId= 3,EventCategoryId=1,Description ="Two days of wine and charcuterie pairings at five Bainbridge Island wineries ", Name = "Wine on the Rock: Wine & Charcuterie ",
Date=new DateTime(12/01/2019),Organizer="Winery Alliance", AddressLine="Five Bainbridge Island wineries" ,City="Bainbridge",State="WA", PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

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
  private static IEnumerable<Ticket> GetPreconfiguredTicket()
        {

            return new List<Ticket>
          {
            new Ticket() {EventId=1,Title="General Admission", Price=100, TotalSeats=50, ReservedSeats=0, AvailableSeats=50 },
            new Ticket() {EventId=2,Title="Gen eral Admission", Price=50, TotalSeats=45, ReservedSeats=0, AvailableSeats=45 },
            new Ticket() {EventId=3,Title="Student", Price= 12, TotalSeats=60, ReservedSeats=0, AvailableSeats=60 },
            new Ticket() {EventId=4,Title= "Free", Price=0, TotalSeats=100, ReservedSeats=50, AvailableSeats=50 },
            new Ticket() {EventId=5,Title="General Admission", Price=80, TotalSeats=120, ReservedSeats=40, AvailableSeats=80 },
            new Ticket() {EventId=6,Title="General Admission ", Price= 20, TotalSeats=37, ReservedSeats= 0, AvailableSeats=37 },
            new Ticket() {EventId=7,Title="Free", Price=0 , TotalSeats=55 , ReservedSeats=5 , AvailableSeats=50 },
            new Ticket() {EventId=8,Title="Student", Price=43, TotalSeats=24 ,ReservedSeats=6 ,AvailableSeats=18 },
            new Ticket() {EventId=9,Title="General Admission", Price=43.8M, TotalSeats=80, ReservedSeats=10, AvailableSeats=70 },
            new Ticket() {EventId=10,Title="Free",Price=0 ,TotalSeats=53 ,ReservedSeats=0, AvailableSeats=53 },
            new Ticket() {EventId=11,Title="Student", Price=45.5M, TotalSeats=32, ReservedSeats=2, AvailableSeats= 30},
            new Ticket() {EventId=12,Title="General Admission", Price=34.4M, TotalSeats=65 ,ReservedSeats=0 ,AvailableSeats=65 },
            new Ticket() {EventId=13,Title="Free",Price=0 ,TotalSeats=200 ,ReservedSeats=50 ,AvailableSeats=150 },
            new Ticket() {EventId=14,Title="General Admission",Price=55.5M ,TotalSeats=20 ,ReservedSeats=0 ,AvailableSeats=20 },
            new Ticket() {EventId=15,Title="General Admission",Price=18 ,TotalSeats=40 ,ReservedSeats=20 ,AvailableSeats=20 },
          };

        }


    }
}
