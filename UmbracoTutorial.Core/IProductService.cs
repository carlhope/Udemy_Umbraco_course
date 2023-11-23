using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoTutorial.Core.Models;

namespace UmbracoTutorial.Core
{
    public interface IProductService
    {
        List<ProductDTO> GetAll();
    }
}
