using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.ProductDTOs;
using ShopApp.Application.UseCases.ProductCases;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(
        CreateProductUseCase createProductUseCase,
        DeleteProductUseCase deleteProductUseCase,
        GetAllProductUseCase getAllProductUseCase,
        GetOneProductUseCase getOneProductUseCase
        ) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult<ProductResponseDTO>> Create([FromBody] CreateProductDTO productDto)
        {
            var product = await createProductUseCase.Execute(productDto);
            return Ok(product);
        }
        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(int productId)
        {
            await deleteProductUseCase.Execute(productId);
            return Ok();
        }
        [HttpGet()]
        public async Task<ActionResult<List<ProductResponseDTO>>> GetAll()
        {
            var productList = await getAllProductUseCase.Execute();
            return Ok(productList);
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponseDTO>> Get(int productId)
        {
            var product = await getOneProductUseCase.Execute(productId);
            return Ok(product);
        }

    }
}