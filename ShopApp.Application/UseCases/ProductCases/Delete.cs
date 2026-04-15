using ShopApp.Application.Exceptions;
using ShopApp.Domain.Interfaces;

namespace ShopApp.Application.UseCases.ProductCases
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        public async Task<bool> Execute(int id)
        {
            var deleted = await productRepository.DeleteAsync(id);
            if (!deleted) throw new ProductNotFoundException(id);

            return true;
        }
    }
}