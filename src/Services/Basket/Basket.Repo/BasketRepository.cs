using Basket.Data.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Repo
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task DeleteAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return new ShoppingCart(userName);

            var deserializedCart = JsonConvert.DeserializeObject<ShoppingCart>(basket);
            if (deserializedCart == null)
                return new ShoppingCart(userName);

            return deserializedCart;
        }

        public async Task<ShoppingCart> UpdateAsync(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetAsync(basket.UserName);
        }
    }
}
