using AutoMapper;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Entities;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class CreateProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public IProductRepository _productRepository = productRepository;
        public IMapper _mapper = mapper;

        public async Task<ProductResponseDTO> Execute(CreateProductDTO dto)
        {
            var existingProduct = await _productRepository.GetOneAsync(dto.Name);
            if (existingProduct != null) throw new ProductAlreadyExistsException(dto.Name);

            var product = new Product
            (
                dto.Name,
                dto.Price,
                dto.StockAmount
            );

            var createdProduct = await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductResponseDTO>(createdProduct);
        }
    }
}