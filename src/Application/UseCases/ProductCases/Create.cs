using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Application.DTOs.ProductDTOs;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.ProductCases
{
    public class CreateProductUseCase(IProductRepository productRepository)
    {
        public IProductRepository _productRepository = productRepository;

        public async Task<Product> Execute(CreateProductDTO dto)
        {
            var existingProduct = await _productRepository.GetOneAsync(dto.Name.Trim());
            if (existingProduct != null) throw new BadHttpRequestException($"Product with such name {dto.Name.Trim()} already exists!");

            var product = new Product
            {
                Name = dto.Name.Trim(),
                Price = dto.Price,
                StockAmount = dto.StockAmount,
            };

            return await _productRepository.CreateAsync(product);
        }
    }
}