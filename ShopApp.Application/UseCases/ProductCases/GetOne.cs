using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class GetOneProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public IProductRepository _productRepository = productRepository;
        public IMapper _mapper = mapper;
        public async Task<ProductResponseDTO> Execute(int id)
        {
            var product = await _productRepository.GetOneAsync(id);
            if (product == null) throw new ProductNotFoundException(id);
            return _mapper.Map<ProductResponseDTO>(product);
        }
    }
}