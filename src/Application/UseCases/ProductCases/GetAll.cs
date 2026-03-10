using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApp.src.Domain.Entities;
using UsersApp.src.Domain.Interfaces;

namespace UsersApp.src.Application.UseCases.ProductCases
{
    public class GetAllProductUseCase(IProductRepository productRepository)
    {
        public IProductRepository _productRepository = productRepository;

        public Task<List<Product>> Execute()
        {
            return _productRepository.GetAllAsync();
        }
    }
}