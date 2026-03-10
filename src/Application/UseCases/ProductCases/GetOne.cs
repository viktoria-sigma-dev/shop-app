using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.ProductCases
{
    public class GetOneProductUseCase(IProductRepository productRepository)
    {
        public IProductRepository _productRepository = productRepository;
        public async Task<Product> Execute(int id)
        {
            var product = await _productRepository.GetOneAsync(id);
            if (product == null) throw new KeyNotFoundException($"Product with such id {id} was not found");
            return product;
        }
    }
}