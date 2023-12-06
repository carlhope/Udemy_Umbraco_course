using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoTutorial.ViewModels;
using UmbracoTutorial.Core.Services;

namespace UmbracoTutorial.Controllers
{
    public class ProductController : UmbracoPageController, IVirtualPageController
    {
        private readonly IProductService _productService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        public ProductController(ILogger<UmbracoPageController> logger, ICompositeViewEngine compositeViewEngine, IProductService productService, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine)
        {
            _productService = productService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public IActionResult Details(int id)
        {
            //productDTO
            var product = _productService.GetAll().FirstOrDefault(x => x.Id == id);

            //if productDTO==null
            if (product == null || CurrentPage == null)
            {
                return NotFound();
            }
            //return NotFound();

            //vm
            var vm = new ProductListingBlockListViewModel(CurrentPage)
            {
                ProductName = product.Name
            };
            //return view(vm)
            return View("Product/Details", vm);
        }

        public IPublishedContent? FindContent(ActionExecutingContext actionExecutingContext)
        {
            var homePage = _umbracoContextAccessor.GetRequiredUmbracoContext()?.Content?.GetAtRoot().FirstOrDefault();

            var productListingPage = homePage?.FirstChildOfType("products");

            return productListingPage ?? homePage;
        }
    }
}
