using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class GetOneProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public async Task<ProductResponseDTO> Execute(int id)
        {
            var product = await productRepository.GetOneAsync(id);
            if (product == null) throw new ProductNotFoundException(id);
            return mapper.Map<ProductResponseDTO>(product);
        }
    }
}