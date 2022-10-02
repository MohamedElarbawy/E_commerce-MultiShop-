using AutoMapper;
using CoreLayer.Entities;
using MVC_Layer.Models;

namespace MVC_Layer.Mapper
{
    public class DomainProfile:Profile
    {

        public DomainProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel,Product>();

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

        }

    }
}
