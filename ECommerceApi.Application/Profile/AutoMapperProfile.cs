using ECommerceApi.Application.DTOs.Dtos;
using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>().ReverseMap();

            //Product
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
