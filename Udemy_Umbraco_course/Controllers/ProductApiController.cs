using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoTutorial.Controllers
{
    // /umbraco/api/productapi/{action}
    public class ProductApiController : UmbracoApiController
    {
        [HttpGet("api/products")] // /umbraco/api/productapi/read
        public IActionResult Read()
        {
            return Ok("read");
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
