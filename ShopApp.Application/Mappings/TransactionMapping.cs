using AutoMapper;
using ShopApp.Application.DTOs.TransactionDTOs;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Mappings
{
    public class TransactionMapping : Profile
    {
        public TransactionMapping()
        {
            CreateMap<Transaction, TransactionResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransactionId));
        }
    }
}