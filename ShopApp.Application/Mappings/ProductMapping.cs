using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Mappings
{
    public class ProductMapping: Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<Product, ProductInOrderResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));;
        }
    }
}