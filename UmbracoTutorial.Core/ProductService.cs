using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoTutorial.Core.Models;

namespace UmbracoTutorial.Core
{
    public class ProductService : IProductService
    {
        public List<ProductDTO> GetAll()
        {
            return new List<ProductDTO> {
               new ProductDTO() { Id = 1, Name = "Product name 1" },
               new ProductDTO() { Id = 2, Name = "Product name 2" },
               new ProductDTO() { Id = 3, Name = "Product name 3" },
               new ProductDTO() { Id = 4, Name = "Product name 4" },
               new ProductDTO() { Id = 5, Name = "Product name 5" },
           };
        }
    }
}
