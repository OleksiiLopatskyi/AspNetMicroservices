using Catalog.API.Core.Entities;
using Catalog.API.DTO.Request;
using Catalog.API.DTO.Response;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseService<Product> _productService;

        public ProductController(IBaseService<Product> productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MinimalProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsAsync()
        {
            return Ok(await _productService.FindAllAsync<MinimalProductResponse>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetProductAsync(string id)
        {
            return Ok(await _productService.FindByIdAsync<Product>(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] AddProductRequest request)
        {
            await _productService.CreateAsync<AddProductRequest>(request);
            return Created("api/products", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateProductRequest request)
        {
            await _productService.UpdateAsync<UpdateProductRequest>(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}