﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoTutorial.Core.Models;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.Repository
{
	public interface IProductRepository
	{
		List<Product> GetProducts(string? productSKU, decimal? maxPrice);

		Product Get(int id);

		Product Create(ProductCreationItem product);

		bool Delete(int id);
	}
}
