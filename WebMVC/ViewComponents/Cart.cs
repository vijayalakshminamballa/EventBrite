﻿using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.ViewComponents
{
    public class Cart : ViewComponent
        {
            private readonly IWishListService _cartSvc;

            public Cart(IWishListService cartSvc) => _cartSvc = cartSvc;
            public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
            {


                var vm = new WishListComponentViewModel();
                try
                {
                    var cart = await _cartSvc.GetWishList(user);

                    vm.ItemsInCart = cart.Items.Count;
                    vm.TotalCost = cart.Total();
                    return View(vm);
                }
                catch (BrokenCircuitException)
                {
                    // Catch error when CartApi is in open circuit mode
                    ViewBag.IsBasketInoperative = true;
                }

                return View(vm);
            }

        }

    }

