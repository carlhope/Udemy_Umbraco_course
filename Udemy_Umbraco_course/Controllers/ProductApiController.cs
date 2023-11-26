using Microsoft.AspNetCore.Mvc;
using Udemy_Umbraco_course.ViewModels.Api;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoTutorial.Core.Repository;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Controllers
{
    // /umbraco/api/productapi/{action}
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
        [HttpGet("api/products")] // /umbraco/api/productapi/read
        public IActionResult Read([FromQuery] ProductReadRequest request)
        {
            var mapped = _mapper.MapEnumerable<Product, ProductApiResponseItem>(_productRepository.GetProducts(request.productSKU,request.maxPrice));
            return Ok(mapped);
        }
        [HttpPost("api/products")] // /umbraco/api/productapi/create
        public IActionResult Create()
        {
            return Ok("create");
        }
        [HttpPut("api/products")] // /umbraco/api/productapi/update
        public IActionResult Update()
        {
            return Ok("update");
        }
        [HttpDelete("api/products")] // /umbraco/api/productapi/delete
        public IActionResult Delete()
        {
            return Ok("delete");
        }
    }
}
