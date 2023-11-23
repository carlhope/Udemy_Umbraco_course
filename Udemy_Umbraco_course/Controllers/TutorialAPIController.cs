using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoTutorial.Controllers
{
    public class TutorialApiController : UmbracoApiController
    {
        public IActionResult GetProducts()
        {
            var products = new List<SimpleProduct>()
            {
                new SimpleProduct() { Id = "1", Title = "Product 1" },
                new SimpleProduct() { Id = "2", Title = "Product 2" },
                new SimpleProduct() { Id = "3", Title = "Product 3" }
            };
            return Ok(products);
        }
        private class SimpleProduct
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }
    }
}
