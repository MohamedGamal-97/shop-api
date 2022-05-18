using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourStoreApi.Models;
using YourStoreApi.Services;

namespace YourStoreApi.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var UpdateBasket = await _basketRepository.UpdateBasketAsync(basket);
            return Ok(UpdateBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }

}
