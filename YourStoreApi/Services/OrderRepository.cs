using YourStoreApi.Models;
using YourStoreApi.Models.OderAggregate;

namespace YourStoreApi.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IBasketRepository BasketRepo;


        public OrderRepository( IUnitOfWork _unitOfWork,IBasketRepository _basketRepo)
        {
            unitOfWork = _unitOfWork;
            BasketRepo = _basketRepo;
        }

        
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,  YourStoreApi.Models.OderAggregate.Address shippingAdress)
        {
            // get basket from repoooooooooooooo
            var basket = await BasketRepo.GetBasketAsync(basketId);

            /// get items from product repo
            var items = new List<OrderItem>();
            foreach(var item in items)
            {
                var ProductItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                 
                var itemOrded = new ProductItemOrdered(ProductItem.Id, ProductItem.Name , ProductItem.ProductImages[0].PictureUrl);
                var orderItem = new OrderItem(itemOrded, ProductItem.Price, item.Quantity);
                items.Add(orderItem);

            }
            // 
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var subTotal = items.Sum(item => item.Price * item.Quantity);
            var order = new Order(buyerEmail, shippingAdress, deliveryMethod, items, subTotal);
            unitOfWork.Repository<Order>().Add(order);
            var result = await unitOfWork.Complete();
            if (result <= 0) return null;
            await BasketRepo.DeleteBasketAsync(basketId);
            return order;
        }

       

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().ListAsync(spec);
        }

        
    }
}
