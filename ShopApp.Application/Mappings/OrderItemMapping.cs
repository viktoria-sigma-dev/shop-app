using AutoMapper;
using ShopApp.Application.DTOs.OrderItemDTOs;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Mappings
{
    public class OrderItemMapping: Profile
    {
        public OrderItemMapping()
        {
            CreateMap<OrderItem, OrderItemResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderItemId))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        }
    }
}