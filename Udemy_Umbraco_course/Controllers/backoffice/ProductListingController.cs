using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.BackOffice.Controllers;
using UmbracoTutorial.Core.Models.Records;
using UmbracoTutorial.Core.Services;
using UmbracoTutorial.Core.UmbracoModels;

namespace Udemy_Umbraco_course.Controllers.backoffice
{
    // /umbraco/backoffice/api/ProductListing/GetProducts?number=X
    public class ProductListingController : UmbracoAuthorizedApiController
    {
        private readonly IProductService _productService;
        public ProductListingController(IProductService productService)
        {
            _productService = productService;
        }
       
        public IActionResult GetProducts(int number)
        {
            return Ok(_productService.GetUmbracoProducts(number));
        }
    }
}
