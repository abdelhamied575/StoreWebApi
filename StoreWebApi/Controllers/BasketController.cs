using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using StoreWeb.Core.Repositories.Contract;
using StoreWebApi.Errors;

namespace StoreWebApi.Controllers
{
  
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public IMapper _Mapper { get; }

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _Mapper = mapper;
        }



        [HttpGet]
        public async Task <ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400, "Invalid Id"));


            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket is null) new CustomerBasket() { Id = id };

            return Ok(basket);

        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto model)
        {
            var basket = await _basketRepository.UpdateBasketAsync(_Mapper.Map<CustomerBasket>(model));

            if (basket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);

        }


        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await _basketRepository.DeleteBasketAsync(id);
        }



    }


}
