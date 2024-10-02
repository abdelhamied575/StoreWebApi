using AutoMapper;
using StoreWeb.Core;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Entities;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
            => _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllAsync());
            
        

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product =  await _unitOfWork.Repository<Product,int>().GetAsync(id);
            var MappedProduct=_mapper.Map<ProductDto>(product);
            return MappedProduct;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
           return _mapper.Map<IEnumerable<TypeBrandDto>>(await   _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        }

        
    }
}
