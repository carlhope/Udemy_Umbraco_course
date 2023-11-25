﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly IUmbracoContextFactory _umbracoContextFactory;
		public ProductRepository(IUmbracoContextFactory umbracoContextFactory)
		{
			_umbracoContextFactory = umbracoContextFactory;
		}
		public List<Product> GetProducts()
		{
			var products = GetProductsRootPage();
			var final = new List<Product>();
			if(products is Products productsRoot)
			{
				final = productsRoot.Children<Product>()?.Where(x=>x.IsPublished())?.ToList() ?? new List<Product>();
			}
			return final;
		}
		private Products? GetProductsRootPage()
		{
			using var cref = _umbracoContextFactory.EnsureUmbracoContext();

			var rootNode = cref?.UmbracoContext?.Content?.GetAtRoot()?.FirstOrDefault(x => x.ContentType.Alias == Home.ModelTypeAlias);

			return rootNode?.Descendant<Products>();
		}
	}
}
