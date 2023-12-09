using Microsoft.AspNetCore.Mvc;
using Udemy_Umbraco_course.ViewModels;
using UmbracoTutorial.Core.Services;


namespace Udemy_Umbraco_course.ViewComponents
{
    public class ProductListingViewComponent: ViewComponent
    {
        private readonly IProductService _productService;
        public ProductListingViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int number)
        {
            var vm = new ProductListingViewModel()
            {
                Products = _productService.GetUmbracoProducts(number)
            };
            return View(vm);

        }

    }
}
