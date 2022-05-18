using YourStoreApi.Models.OderAggregate;

namespace YourStoreApi.Services
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod,
            string basketId, Address shippingAdress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();

    }
}
