using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.DomainLayer.Model;
using WebApi.DomainLayer.Services;
using WebApi.RestApi.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.RestApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            _logger?.LogDebug($"Method '{nameof(GetAllProducts)}' called.");
            var products = await _productService.GetAllProducts();
            _logger?.LogInformation($"Method '{nameof(GetAllProducts)}' successfully done.");
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            _logger?.LogDebug($"Method '{nameof(GetProduct)}' ID: {id} called.");
            var product = await _productService.GetProductById(id);
            _logger?.LogInformation($"Method '{nameof(GetProduct)}' ID: {id} successfully done.");
            return Ok(product);
        }

        [HttpPut("{id}/description")]
        public async Task<IActionResult> UpdateProductDescription(int id, [FromBody] ProductDescription productDescription)
        {
            _logger?.LogDebug($"Method '{nameof(UpdateProductDescription)}' ID: {id}, new description: '{productDescription.Description}' called.");
            if (id < 0) return BadRequest("Bad ID");
            var productToUpdate = await _productService.GetProductById(id);
            if (productToUpdate == null) return NotFound();
            await _productService.UpdateDescription(productToUpdate, productDescription.Description);
            _logger?.LogInformation($"Method '{nameof(UpdateProductDescription)}' ID: {id} successfully done.");
            return NoContent();
        }
    }
}
