using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.ProductCases
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        public IProductRepository _productRepository = productRepository;

        public async Task<bool> Execute(int id)
        {
            var deleted = await _productRepository.DeleteAsync(id);
            if (!deleted) throw new KeyNotFoundException($"Product {id} does not exist!");

            return true;
        }
    }
}