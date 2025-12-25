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
            CreateMap<Product, ProductDTO>();

            //DTO -> Entity
            CreateMap<CreateProductDTO, Product>();
        }
    }
}
