using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public interface IWishListRepository
    {
        Task<WishList> GetWishListAsync(string cartId);
        IEnumerable<string> GetUsers();
        Task<WishList> UpdateWishListAsync(WishList basket);
        Task<bool> DeleteWishListAsync(string id);

    }
}
