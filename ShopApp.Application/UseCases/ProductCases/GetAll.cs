using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class GetAllProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public IProductRepository _productRepository = productRepository;
        public IMapper _mapper = mapper;

        public async Task<List<ProductResponseDTO>> Execute()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductResponseDTO>>(products);
        }
    }
}