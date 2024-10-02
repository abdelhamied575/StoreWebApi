using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Services.Contract;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet] // BaseUrl/api/Products
        public async Task<IActionResult> GetAllProducts() // EndPoint
        {
            var result = await _productService.GetAllProductAsync();

            return Ok(result);

        }



        [HttpGet("brands")] // BaseUrl/api/Products
        //[Route("api/[controller]/Brands")]
        public async Task<IActionResult> GetAllBrands() // EndPoint
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);

        }

        [HttpGet("types")] // BaseUrl/api/Products
        //[Route("api/[controller]/Brands")]
        public async Task<IActionResult> GetAllTypes() // EndPoint
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);

        }



        [HttpGet("id")] // BaseUrl/api/Products
        public async Task<IActionResult> GetProductById(int? id) // EndPoint
        {
            if (id is null) return BadRequest("Invalid id !!");

            var result = await _productService.GetProductByIdAsync(id.Value);

            if (result is null) return NotFound($"The Product With Id :{id} Not Found At DataBase");

            return Ok(result);

        }



    }
}
