﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoTutorial.Core.Models;
using UmbracoTutorial.Core.Models.Records;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        public ProductService(IUmbracoContextFactory umbracoContextFactory) 
        { 
            _umbracoContextFactory = umbracoContextFactory;
        }
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
        public List<ProductResponseItem> GetUmbracoProducts(int number)
        {
            var final = new List<ProductResponseItem>();
            using (var cref = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var contentCache = cref.UmbracoContext.Content;

                var products = contentCache
                    ?.GetAtRoot()
                    ?.FirstOrDefault(x => x.ContentType.Alias == Home.ModelTypeAlias)
                    ?.Descendant<Products>()
                    ?.Children<Product>()
                    ?.Take(number);

                if (products != null && products.Any())
                {
                    final = products.Select(x => new ProductResponseItem(x.Id, x?.ProductName ?? x.Name, x?.Photos?.Url() ?? "#")).ToList();
                }
                return final;
            }
        }
    }
}
