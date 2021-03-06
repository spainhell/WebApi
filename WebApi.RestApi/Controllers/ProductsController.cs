﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.DomainLayer.Model;
using WebApi.DomainLayer.Services;
using WebApi.RestApi.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.RestApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        // GET: api/v1/Products
        /// <summary>
        /// Returns all products list
        /// </summary>
        /// <returns>A response with products list</returns>
        [HttpGet(""), MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger?.LogDebug($"Method '{nameof(GetAllProducts)}' called.");
            try
            {
                var products = await _productService.GetAllProducts();
                _logger?.LogInformation($"Method '{nameof(GetAllProducts)}' successfully done.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical($"Method '{nameof(GetAllProducts)}' error: {ex.Message}.");
                throw;
            }
        }

        // GET: api/v1/Products/5
        /// <summary>
        /// Returns a specific product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return a product</response>
        /// <response code="404">Product not found</response> 
        /// <returns>A response with a product</returns>
        [HttpGet("{id}"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (id < 0) return BadRequest("Bad ID");
            _logger?.LogDebug($"Method '{nameof(GetProduct)}' ID: {id} called.");
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null) return NotFound();
                _logger?.LogInformation($"Method '{nameof(GetProduct)}' ID: {id} successfully done.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical($"Method '{nameof(GetProduct)}' ID: {id} error: {ex.Message}.");
                throw;
            }
        }

        // PUT: api/v1/Products/5
        /// <summary>
        /// Updates a product description
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/products/{id}/description
        ///     {
        ///        "description": "new product description"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="productDescription"></param>
        /// <response code="204">Product description was successfully updated</response>
        /// <response code="404">Product not found</response>
        /// <returns>N/A</returns>
        [HttpPut("{id}/description"), MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductDescription(int id, [FromBody] ProductDescription productDescription)
        {
            _logger?.LogDebug($"Method '{nameof(UpdateProductDescription)}' ID: {id}, new description: '{productDescription.Description}' called.");
            if (id < 0) return BadRequest("Bad ID");
            try
            {
                var productToUpdate = await _productService.GetProductById(id);
                if (productToUpdate == null) return NotFound();
                await _productService.UpdateDescription(productToUpdate, productDescription.Description);
                _logger?.LogInformation($"Method '{nameof(UpdateProductDescription)}' ID: {id} successfully done.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.LogCritical($"Method '{nameof(UpdateProductDescription)}' ID: {id} error: {ex.Message}.");
                throw;
            }
        }


        /* ********** API V2 ********** */
        
        // GET: api/v2/Products/paged?pageNumber=1&pageSize=10
        /// <summary>
        /// Returns products page
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v2/products/paged?pageNumber=1&pageSize=10
        ///
        /// </remarks>
        /// <param name="pageNumber">Requested page number</param>
        /// <param name="pageSize">Requested page size. Default value is 10.</param>
        /// <response code="200">Return selected products</response>
        /// <response code="404">Invalid query parameters</response>
        /// <returns>A response with products page</returns>
        [HttpGet("paged")]
        [MapToApiVersion("2.0")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProductsV2([FromQuery][Required] int pageNumber, [FromQuery]int pageSize = 10)
        {
            _logger?.LogDebug($"Method '{nameof(GetAllProductsV2)}' called. PageNumber {pageNumber} / PageSize {pageSize}.");
            if (pageNumber < 1 || pageSize < 1) return (BadRequest("pageNumber or pageSize less than 1"));
            try
            {
                List<Product> products = (await _productService.GetAllProductsPage(pageNumber, pageSize)).ToList();
                var itemsCount = products.Count();
                _logger?.LogInformation($"Method '{nameof(GetAllProductsV2)}' successfully done.");
                return Ok(new PagedResponse<Product>(pageNumber, pageSize, itemsCount, products));
            }
            catch (Exception ex)
            {
                _logger?.LogCritical($"Method '{nameof(GetAllProductsV2)}' error: {ex.Message}.");
                throw;
            }
        }
    }
}
