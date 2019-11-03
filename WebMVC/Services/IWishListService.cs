using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Models.BookingModels;
using WebMVC.Models.WishListModels;

namespace WebMVC.Services
{
   public interface IWishListService
   {
        Task<WishList> GetWishList(ApplicationUser user);
        Task AddItemToWishList(ApplicationUser user, WishListItem product);
        Task<WishList> UpdateWishList(WishList Cart);
        Task<WishList> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Order MapCartToOrder(WishList Cart);
        Task ClearWishList(ApplicationUser user);
   }
}

