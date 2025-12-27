using AutoMapper;
using ProductManager.API.DTOs;
using ProductManager.Core.Entities;

namespace ProductManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Entity -> DTO
            CreateMap<Product, ProductDTO>().ForMember(dest=>dest.CategoryName, otp => otp.MapFrom(src=>src.Category.Name));

            //DTO -> Entity
            CreateMap<CreateProductDTO, Product>();


            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
