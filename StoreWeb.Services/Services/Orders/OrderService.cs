using StoreWeb.Core;
using StoreWeb.Core.Entities;
using StoreWeb.Core.Entities.Order;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public OrderService(IUnitOfWork unitOfWork,IBasketService basketService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }


        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {

            var basket = await  _basketService.GetBasketAsync(basketId);


            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            if(basket.Items.Count()> 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);

                    var productOrderItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productOrderItem, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            } 


            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

            var subTotal=orderItems.Sum(I=>I.Price*I.Quantity);

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, "");

            await _unitOfWork.Repository<Order,int>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync(); 

            if(result<=0) return null;

            return order;

        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec= new OrderSpecifications(buyerEmail, orderId);

            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            if(order is null) return null;

            return order;



        }

        public async Task<IEnumerable<Order>?> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            
            var spec= new OrderSpecifications(buyerEmail);
            
            var orders= await _unitOfWork.Repository<Order,int>().GetAllWithSpecAsync(spec);

            if(orders is null) return null;

            return orders;



        }
   
    
    }
}
