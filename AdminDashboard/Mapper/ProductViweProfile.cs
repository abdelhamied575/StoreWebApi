using AdminDashboard.Models;
using AutoMapper;
using StoreWeb.Core.Entities;

namespace AdminDashboard.Mapper
{
	public class ProductViweProfile:Profile
	{

        public ProductViweProfile()
        {
            CreateMap<Product, ProductFormViewModel>().ReverseMap();
        }


    }
}
