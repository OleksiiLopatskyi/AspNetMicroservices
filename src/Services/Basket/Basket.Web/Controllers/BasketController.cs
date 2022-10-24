using Basket.Data.Entities;
using Basket.Repo;
using Microsoft.AspNetCore.Mvc;
using OLopatskyi.ErrorsHandler.Exceptions;
using System.Net;

namespace Basket.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string userName)
        {
            var basket = await _repository.GetAsync(userName);
            return Ok(basket);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody]ShoppingCart shoppingCart)
        {
            var updatedBasket = await _repository.UpdateAsync(shoppingCart);
            return Ok(updatedBasket);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(string userName)
        {
            await _repository.DeleteAsync(userName);
            return NoContent();
        }
    }
}