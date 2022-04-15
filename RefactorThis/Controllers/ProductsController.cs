using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactorThis.Common.Exceptions;
using RefactorThis.Dto;
using RefactorThis.Providers;
using RefactorThis.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace RefactorThis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IProductProvider _productProvider;
        private ProductValidator _productValidator;
        private IProductOptionProvider _productOptionProvider;
        private ProductOptionValidator _productOptionValidator;

        public ProductsController(ILogger<ProductsController> logger,
            IProductProvider productProvider, IProductOptionProvider productOptionProvider,
            ProductValidator productValidator, ProductOptionValidator productOptionValidator)
        {
            _logger = logger;
            _productProvider = productProvider;
            _productValidator = productValidator;
            _productOptionProvider = productOptionProvider;
            _productOptionValidator = productOptionValidator;
        }

        /// <summary>
        /// Health Check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("health")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult HealthCheck()
        {
            return Ok();
        }

        /// <summary>
        /// gets all products.
        /// </summary>
        /// <returns>List<ProductDto></returns>
        [HttpGet]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        public ActionResult<List<ProductDto>> Get()
        {
            try {
                _logger.LogInformation($"Fetching all products");
                return Ok(_productProvider.Get());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered for request Get all products: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// gets the project that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>ProductDto</returns>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        public ActionResult<ProductDto> Get(Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching product for Id {id}");
                return Ok(_productProvider.Get(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while get product with Id {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">ProductDto</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult Post([FromBody] ProductDto product)
        {
            try
            {
                _logger.LogInformation($"Posting product with name {product.Name}");
                var result = _productValidator.Validate(product);

                if (!result.IsValid)
                    return BadRequest(result.Errors);

                _productProvider.Save(product);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while posting product with name {product.Name}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="product">ProductDto</param>
        /// <returns>ActionResult</returns>
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult Update(Guid id, [FromBody] ProductDto product)
        {
            try
            {
                _logger.LogInformation($"Updating product with name {product.Name}");

                var result = _productValidator.Validate(product);

                if (!result.IsValid)
                    return BadRequest(result.Errors);

                _productProvider.Update(id, product);
                return Ok();
            }
            catch(BadRequestException ex)
            {
                _logger.LogInformation($"Error encountered while updating product with name {product.Name}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while updating product with name {product.Name}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting product with id {id}");
                _productProvider.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while deleting product with id {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all options for a product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>List<ProductOptionDto></returns>
        [HttpGet("{productId}/options")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type=typeof(List<ProductOptionDto>))]
        public ActionResult<List<ProductOptionDto>> GetOptions(Guid productId)
        {
            
            try
            {
                _logger.LogInformation($"Fetching product option for product id {productId}");
                return Ok(_productOptionProvider.GetByProductId(productId));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while fetching product option for product id  {productId}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get options for a product for specific product and option
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="id">Product option Id</param>
        /// <returns>ProductOptionDto</returns>
        [HttpGet("{productId}/options/{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, Type=typeof(ProductOptionDto))]
        public ActionResult<ProductOptionDto> GetOption(Guid productId, Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching product option for product id {productId} and id {id}");
                return Ok(_productOptionProvider.GetByProductIdAndId(productId, id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while fetching product option for product id  {productId} and id {id}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add new options for a product for specific product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="option">ProductOptionDto</param>
        /// <returns>ActionResult</returns>
        [HttpPost("{productId}/options")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult CreateOption(Guid productId, [FromBody] ProductOptionDto option)
        {
            try
            {
                _logger.LogInformation($"Posting product option with name {option.Id}");
                var result = _productOptionValidator.Validate(option);

                if (!result.IsValid)
                    return BadRequest(result.Errors);

                _productOptionProvider.Save(productId, option);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while posting product option with name {option.Name}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates an option for a product for specific product and option
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="id">option Id</param>
        /// <param name="option">ProductOptionDto</param>
        /// <returns>ActionResult</returns>
        [HttpPut("{productId}/options/{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult UpdateOption(Guid productId, Guid id, [FromBody] ProductOptionDto option)
        {
            try
            {
                _logger.LogInformation($"Posting product option with name {option.Id}");
                var result = _productOptionValidator.Validate(option);

                if (!result.IsValid)
                    return BadRequest(result.Errors);

                _productOptionProvider.Update(productId, id, option);
                return StatusCode(200);
            }
            catch (BadRequestException ex)
            {
                _logger.LogInformation($"Error encountered while updating product with name {option.Name}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while posting product option with name {option.Name}: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes an option for a product for specific product and option
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="id">option Id</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{productId}/options/{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public ActionResult DeleteOption(Guid productId, Guid id)
        {
            try
            {
                _logger.LogInformation($"Posting product option with name {id}");
                

                _productOptionProvider.Delete(productId, id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error encountered while posting product option with name {id}: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
