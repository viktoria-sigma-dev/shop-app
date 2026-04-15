using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class GetAllProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public async Task<List<ProductResponseDTO>> Execute()
        {
            var products = await productRepository.GetAllAsync();
            return mapper.Map<List<ProductResponseDTO>>(products);
        }
    }
}