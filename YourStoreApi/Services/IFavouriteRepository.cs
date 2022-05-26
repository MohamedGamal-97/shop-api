using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public interface IFavouriteRepository
    {

        Task<CustomerFavourite> GetBasketAsync(string favouriteId);
        Task<CustomerFavourite> UpdateBasketAsync(CustomerFavourite favourite);
        Task<bool> DeleteBasketAsync(string favouriteId);
    }
}