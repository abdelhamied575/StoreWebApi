using StoreWeb.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Services.Contract
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>>GetAllProductAsync();
        Task<IEnumerable<TypeBrandDto>>GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>>GetAllBrandsAsync();

        Task<ProductDto> GetProductByIdAsync(int id);






    }
}
