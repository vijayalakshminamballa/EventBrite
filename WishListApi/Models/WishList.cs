using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class WishList
    {
        public string BuyerId { get; set; }
        public List<WishListItem> Items { get; set; }
        public WishList(string cartId)
        {
            BuyerId = cartId;
            Items = new List<WishListItem>();
        }
    }
}
