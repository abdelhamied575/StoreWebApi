using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Helper;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core.Specifications.Products;

namespace StoreWebApi.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet] // BaseUrl/api/Products
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec ) // EndPoint
        {
            var result = await _productService.GetAllProductAsync(productSpec);

            //return Ok(new PaginationResponse<ProductDto>(productSpec.PageSize,productSpec.PageIndex,0,result));
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



        [HttpGet("{id}")] // BaseUrl/api/Products/1
        public async Task<IActionResult> GetProductById(int? id) // EndPoint
        {
            if (id is null) return BadRequest("Invalid id !!");

            var result = await _productService.GetProductByIdAsync(id.Value);

            if (result is null) return NotFound($"The Product With Id :{id} Not Found At DataBase");

            return Ok(result);

        }



    }
}
