using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersApp.src.Application.DTOs.ProductDTOs;
using UsersApp.src.Application.UseCases.ProductCases;
using UsersApp.src.Domain.Entities;

namespace UsersApp.src.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductUseCase _createProductUseCase;
        private readonly DeleteProductUseCase _deleteProductUseCase;
        private readonly GetAllProductUseCase _getAllProductUseCase;
        private readonly GetOneProductUseCase _getOneProductUseCase;
        public ProductController(
            CreateProductUseCase createProductUseCase,
            DeleteProductUseCase deleteProductUseCase,
            GetAllProductUseCase getAllProductUseCase,
            GetOneProductUseCase getOneProductUseCase
        )
        {
            _createProductUseCase = createProductUseCase;
            _deleteProductUseCase = deleteProductUseCase;
            _getAllProductUseCase = getAllProductUseCase;
            _getOneProductUseCase = getOneProductUseCase;
        }

        [HttpPost()]
        public async Task<ActionResult<Product>> Create([FromBody] CreateProductDTO productDto)
        {
            var product = await _createProductUseCase.Execute(productDto);
            return Ok(product);
        }
        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(int productId)
        {
            await _deleteProductUseCase.Execute(productId);
            return Ok();
        }
        [HttpGet()]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var productList = await _getAllProductUseCase.Execute();
            return Ok(productList);
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> Get(int productId)
        {
            var product = await _getOneProductUseCase.Execute(productId);
            return Ok(product);
        }

    }
}