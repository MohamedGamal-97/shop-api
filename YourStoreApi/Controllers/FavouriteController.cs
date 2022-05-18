#nullable disable
using Microsoft.AspNetCore.Mvc;
using YourStoreApi.Models;
using YourStoreApi.Services;

namespace YourStoreApi.Controllers
{
    public class FavouriteController:BaseApiController
    {
         private readonly IFavouriteRepository _favouriteRepository;
        public FavouriteController(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerFavourite>> GetBasketById(string id)
        {
            var favourite = await _favouriteRepository.GetBasketAsync(id);

            return Ok(favourite ?? new CustomerFavourite(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerFavourite>> UpdateBasket(CustomerFavourite basket)
        {
            var updatedFavourite = await _favouriteRepository.UpdateBasketAsync(basket);

            return Ok(updatedFavourite);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _favouriteRepository.DeleteBasketAsync(id);
        }
    }
}