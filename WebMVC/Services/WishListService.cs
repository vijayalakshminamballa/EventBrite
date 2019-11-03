using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;
using WebMVC.Models.BookingModels;
using WebMVC.Models.WishListModels;

namespace WebMVC.Services
{
    public class WishListService:IWishListService
    {
        private readonly IConfiguration _config;
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly ILogger _logger;
        private IHttpContextAccessor _httpContextAccesor;

        public WishListService(IConfiguration config,
            IHttpContextAccessor httpContextAccesor,
    IHttpClient httpClient,
    ILoggerFactory logger)
        {
            _config = config;
            _remoteServiceBaseUrl = $"{_config["WishListUrl"]}/api/v1/WishList";
            _apiClient = httpClient;
            _logger = logger.CreateLogger<WishListService>();
            _httpContextAccesor = httpContextAccesor;

        }


        public async Task AddItemToWishList(ApplicationUser user,
            WishListItem product)
        {
            var cart = await GetWishList(user);
            _logger.LogDebug("User Name: " + user.Id);
            if (cart == null)
            {
                cart = new WishList()
                {
                    BuyerId = user.Id,
                    Items = new List<WishListItem>()
                };
            }
            var basketItem = cart.Items
                .Where(p => p.ProductId == product.ProductId)
                .FirstOrDefault();
            if (basketItem == null)
            {
                cart.Items.Add(product);
            }
            else
            {
                basketItem.Quantity += 1;
            }


            await UpdateWishList(cart);
        }

        public async Task<WishList> UpdateWishList(WishList cart)
        {

            var token = await GetUserTokenAsync();
            _logger.LogDebug("Service url: " + _remoteServiceBaseUrl);
            var updateBasketUri = ApiPaths.Basket.UpdateBasket(_remoteServiceBaseUrl);
            _logger.LogDebug("Update Basket url: " + updateBasketUri);
            var response = await _apiClient.PostAsync(updateBasketUri, cart, token);
            response.EnsureSuccessStatusCode();

            return cart;
        }

        public async Task<WishList> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var basket = await GetWishList(user);

            basket.Items.ForEach(x =>
            {
                // Simplify this logic by using the
                // new out variable initializer.
                if (quantities.TryGetValue(x.Id, out var quantity))
                {
                    x.Quantity = quantity;
                }
            });

            return basket;
        }

        public async Task ClearWishList(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var cleanBasketUri = ApiPaths.Basket.CleanBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogDebug("Clean Basket uri : " + cleanBasketUri);
            var response = await _apiClient.DeleteAsync(cleanBasketUri);
            _logger.LogDebug("Basket cleaned");
        }


        public async Task<WishList> GetWishList(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            _logger.LogInformation(" We are in get basket and user id " + user.Id);
            _logger.LogInformation(_remoteServiceBaseUrl);

            var getBasketUri = ApiPaths.Basket.GetBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogInformation(getBasketUri);
            var dataString = await _apiClient.GetStringAsync(getBasketUri, token);
            _logger.LogInformation(dataString);

            var response = JsonConvert.DeserializeObject<WishList>(dataString.ToString()) ??
               new WishList()
               {
                   BuyerId = user.Id
               };
            return response;
        }
        public Order MapCartToOrder(WishList cart)
        {
            var order = new Order();
            order.OrderTotal = 0;

            cart.Items.ForEach(x =>
            {
                order.OrderItems.Add(new OrderItem()
                {
                    ProductId = int.Parse(x.ProductId),

                    PictureUrl = x.PictureUrl,
                    ProductName = x.ProductName,
                    Units = x.Quantity,
                    UnitPrice = x.UnitPrice
                });
                order.OrderTotal += (x.Quantity * x.UnitPrice);
            });

            return order;
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");

        }

    }
}
