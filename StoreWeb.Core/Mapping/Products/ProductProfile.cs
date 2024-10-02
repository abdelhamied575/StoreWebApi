using AutoMapper;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Mapping.Products
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D=>D.BrandName,options=>options.MapFrom(S=>S.Brand.Name))
                .ForMember(D=>D.TypeName,options=>options.MapFrom(S=>S.Type.Name));


            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }


    }
}
