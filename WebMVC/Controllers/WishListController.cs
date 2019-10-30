using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Models.WishListModels;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize]

    public class WishListController : Controller
    {
        private readonly IWishListService _cartService;
        private readonly ICatalogService _catalogService;
        private readonly IIdentityService<ApplicationUser> _identityService;

        public WishListController(IIdentityService<ApplicationUser> identityService, IWishListService cartService, ICatalogService catalogService)
        {
            _identityService = identityService;
            _cartService = cartService;
            _catalogService = catalogService;

        }


        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(
            Dictionary<string, int> quantities,
            string action)
        {
            //if (action == "[ Checkout ]")
            //{
            //    return RedirectToAction("Create", "Order");
            //}


            try
            {
                var user = _identityService.Get(HttpContext.User);
                var basket = await _cartService.SetQuantities(user, quantities);
                var vm = await _cartService.UpdateWishList(basket);

            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit  mode                 
                HandleBrokenCircuitException();
            }

            return View();

        }

        public async Task<IActionResult> AddToWishList(EventItem productDetails)
        {
            try
            {
                if (productDetails.Id != null)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new WishListItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Quantity = 1,
                        ProductName = productDetails.Name,
                        PictureUrl = productDetails.PictureUrl,
                        UnitPrice = productDetails.Price,
                        ProductId = productDetails.Id
                    };
                    await _cartService.AddItemToWishList(user, product);
                }
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in circuit-opened mode                 
                HandleBrokenCircuitException();
            }

            return RedirectToAction("Index", "WishList");

        }


        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }


    }

    
}
