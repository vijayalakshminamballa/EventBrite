using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WishListApi.Models;

namespace WishListApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IWishListRepository _repository;
        private readonly ILogger _logger;

        public WishListController(IWishListRepository repository,
            ILoggerFactory logger)
        {
            _repository = repository;
            _logger = logger.CreateLogger<WishListController>();

        }

        // GET api/v1/cart/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WishList), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetWishListAsync(id);

            return Ok(basket);
        }

        // POST api/v1/cart
        [HttpPost]

        [ProducesResponseType(typeof(WishList), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]WishList value)
        {
            var basket = await _repository.UpdateWishListAsync(value);

            return Ok(basket);
        }

        // DELETE api/v1/cart/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _logger.LogInformation("Delete method in Cart controller reached");
            _repository.DeleteWishListAsync(id);


        }


    }

}