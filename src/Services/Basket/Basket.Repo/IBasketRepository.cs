using Basket.Data.Entities;

namespace Basket.Repo
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetAsync(string userName);

        Task<ShoppingCart> UpdateAsync(ShoppingCart basket);

        Task DeleteAsync(string userName);
    }
}
