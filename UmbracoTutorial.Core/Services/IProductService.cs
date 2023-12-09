using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoTutorial.Core.Models;
using UmbracoTutorial.Core.Models.Records;

namespace UmbracoTutorial.Core.Services
{
    public interface IProductService
    {
        List<ProductDTO> GetAll();
        List<ProductResponseItem> GetUmbracoProducts(int number);
    }
}
