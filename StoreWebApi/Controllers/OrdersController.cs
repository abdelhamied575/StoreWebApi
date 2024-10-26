using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Orders;
using StoreWeb.Core.Entities.Order;
using StoreWeb.Core.Services.Contract;
using StoreWebApi.Errors;
using System.Security.Claims;

namespace StoreWebApi.Controllers
{
    
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper )
        {
            _orderService = orderService;
            _mapper = mapper;
        }



        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto model)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if(userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

             var address = _mapper.Map<Address>(model.shipToAddress);


            var order = await _orderService.CreateOrderAsync(userEmail,model.BasketId,model.DeliveryMethodId, address);

            if(order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));



            return Ok(_mapper.Map<OrderToReturnDto>(order));


        }












    }
}
