using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Udemy_Umbraco_course.ViewModels.Api;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoTutorial.Core.Models;
using UmbracoTutorial.Core.Repository;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Controllers
{
    // /umbraco/api/productapi/{action} default route
    [Route("api/products")] //new route
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ProductApiController : UmbracoApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IUmbracoMapper _mapper;
        public ProductApiController(IProductRepository productRepository,  IUmbracoMapper mapper)
        {
			_productRepository = productRepository;
            _mapper = mapper;
		}

        public record ProductReadRequest(string? productSKU, decimal? maxPrice);
        [HttpGet] // /umbraco/api/productapi/read
        public IActionResult Read([FromQuery] ProductReadRequest request)
        {
            var mapped = _mapper.MapEnumerable<Product, ProductApiResponseItem>(_productRepository.GetProducts(request.productSKU,request.maxPrice));
            return Ok(mapped);
        }
        [HttpPost] // /umbraco/api/productapi/create
        public IActionResult Create([FromBody]ProductCreationItem request)
        {
            if(!ModelState.IsValid)
            {
				return BadRequest("Fields error");
			}
            var product = _productRepository.Create(request);
            if(product == null)
            {
                StatusCode(StatusCodes.Status500InternalServerError, $"error creating product");
            }
			return Ok(_mapper.Map<Product, ProductApiResponseItem>(product));
        }
        [HttpPut("{id:int}")] // /umbraco/api/productapi/update
        public IActionResult Update(int id, [FromBody]ProductUpdateItem request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_productRepository.Get(id) == null)
            {
				return NotFound($"product with id {id} not found");
			}
            var product = _productRepository.Update(id, request);
			return Ok(_mapper.Map<Product, ProductApiResponseItem>(product));
		}
        [HttpDelete("{id:int}")] // /umbraco/api/productapi/delete
        public IActionResult Delete(int id)
        {
			if (_productRepository.Get(id) == null)
			{
				return NotFound($"product with id {id} not found");
			}
			var result = _productRepository.Delete(id);
            return result ? Ok("deleted") : StatusCode(StatusCodes.Status500InternalServerError, $"error deleting product with id {id}");
        }
    }
}
