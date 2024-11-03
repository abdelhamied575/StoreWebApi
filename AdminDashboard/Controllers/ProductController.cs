using AdminDashboard.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Entities;
using StoreWeb.Core.Specifications.Products;
using StoreWeb.Services.Helper;

namespace AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task<IActionResult> Index()
        {
			//var spec = new ProductSpecifications(new ProductSpecParams()
   //         {
   //             PageSize=50
   //         });

			//var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);


			var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return View(mappedProducts);
        }



        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {

            var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<ProductFormViewModel>(product);

            return View(mappedProduct);


        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.Picture != null)
                {
                    model.PictureUrl = DocumentSettings.UploadFile(model.Picture, "products");
                }

                var mappedProduct=_mapper.Map<Product>(model);

                await _unitOfWork.Repository<Product,int>().AddAsync(mappedProduct);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");

            }

            return View(model);


        }







    }
}
