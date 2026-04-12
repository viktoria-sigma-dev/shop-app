using AutoMapper;
using ShopApp.Domain.Entities;
using ShopApp.Application.DTOs.UserDTOs;

namespace ShopApp.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
        }
    }
}