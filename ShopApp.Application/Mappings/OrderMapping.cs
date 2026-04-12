using AutoMapper;
using ShopApp.Application.DTOs.OrderDTOs;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Mappings
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, OrderResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}