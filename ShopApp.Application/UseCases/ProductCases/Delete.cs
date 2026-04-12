using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        public IProductRepository _productRepository = productRepository;

        public async Task<bool> Execute(int id)
        {
            var deleted = await _productRepository.DeleteAsync(id);
            if (!deleted) throw new ProductNotFoundException(id);

            return true;
        }
    }
}