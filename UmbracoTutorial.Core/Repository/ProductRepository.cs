using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoTutorial.Core.Models;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly Guid _productsMediaFolder = Guid.Parse("1ad4e9a3-4ad5-476c-aba2-9602e34e323f");

		private readonly IUmbracoContextFactory _umbracoContextFactory;
		private readonly IContentService _contentService;

		private readonly IMediaService _mediaService;

		private readonly MediaFileManager _mediaFileManager;
		private readonly IShortStringHelper _shortStringHelper;
		private readonly MediaUrlGeneratorCollection _mediaUrlGenerators;
		private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
		private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

		private string _productNameAlias = string.Empty;
		private string _productPriceAlias = string.Empty;
		private string _productCategoriesAlias = string.Empty;
		private string _productDescriptionAlias = string.Empty;
		private string _productSkuAlias = string.Empty;
		private string _productPhotoAlias = string.Empty;
		public ProductRepository(
			IUmbracoContextFactory umbracoContextFactory,
			IContentService contentService,
			IMediaService mediaService,
			MediaFileManager mediaFileManager,
			IShortStringHelper shortStringHelper,
			MediaUrlGeneratorCollection mediaUrlGenerators,
			IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,
			IPublishedSnapshotAccessor publishedSnapshotAccessor
			
			)
		{
			_umbracoContextFactory = umbracoContextFactory;
			_contentService = contentService;
			_mediaService = mediaService;
			_mediaFileManager = mediaFileManager;
			_shortStringHelper = shortStringHelper;
			_mediaUrlGenerators = mediaUrlGenerators;
			_contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
			_publishedSnapshotAccessor = publishedSnapshotAccessor;

			_productNameAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.ProductName).Alias;
			_productPriceAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Price).Alias;
			_productCategoriesAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Category).Alias;
			_productDescriptionAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Description).Alias;
			_productSkuAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Sku).Alias;
			_productPhotoAlias = Product.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Photos).Alias;


		}

		

		public List<Product> GetProducts(string? productSKU, Decimal?maxPrice)
		{
			var products = GetProductsRootPage();
			var final = new List<Product>();
			if(products is Products productsRoot)
			{
				var filteredProducts = productsRoot.Children<Product>()?.Where(x => x.IsPublished());
				if (!string.IsNullOrEmpty(productSKU))
				{
					filteredProducts = filteredProducts?.Where(x => x.Sku.InvariantEquals(productSKU));
				}
				if(maxPrice is decimal MaxPrice)
				{
					filteredProducts = filteredProducts?.Where(x => x.Price <= MaxPrice);
				}
				final = filteredProducts?.ToList()??new List<Product>();
			}
			return final;
		}

		public bool Delete(int id)
		{
			var product = _contentService.GetById(id);
            if (product!=null)
            {
				var result = _contentService.Delete(product); 
				return result.Success;
			}
			return false;
        }
		private Products? GetProductsRootPage()
		{
			using var cref = _umbracoContextFactory.EnsureUmbracoContext();

			var rootNode = cref?.UmbracoContext?.Content?.GetAtRoot()?.FirstOrDefault(x => x.ContentType.Alias == Home.ModelTypeAlias);

			return rootNode?.Descendant<Products>();
		}

		public Product Get(int id)
		{
			using(var cref = _umbracoContextFactory.EnsureUmbracoContext())
			{
				var content = cref.UmbracoContext.Content;
				return (Product)content.GetById(id);
			}
		}

		public Product Create(ProductCreationItem product)
		{
			var productImage = CreateProductImage(product.PhotoFileName, product.Photo);
			if(productImage == null)
			{
				return null;
			}
			var productsRoot = GetProductsRootPage();
			var productContent = _contentService.Create(product.ProductName, productsRoot.Key, Product.ModelTypeAlias);

	

			productContent.SetValue(_productNameAlias, product.ProductName);
			productContent.SetValue(_productPriceAlias, product.Price);
			productContent.SetValue(_productCategoriesAlias, string.Join(",", product.Categories));
			productContent.SetValue(_productDescriptionAlias, product.Description);
			productContent.SetValue(_productSkuAlias, product.SKU);
			productContent.SetValue(_productPhotoAlias, productImage);

			_contentService.SaveAndPublish(productContent);

			return Get(productContent.Id);
		}
		private GuidUdi? CreateProductImage(string filename, string photo)
		{
			//Save image to a tmp path
			var tmpFilePath = Path.GetTempFileName();
			using var image = SixLabors.ImageSharp.Image.Load(Convert.FromBase64String(photo));
			image.Save(tmpFilePath, new JpegEncoder());

			//load file into a filestream
			var fileInfo = new FileInfo(tmpFilePath);
			using var fileStream = fileInfo.OpenReadWithRetry();
			if(fileStream == null)
			{
				throw new InvalidOperationException("Could not open file stream");
			}

			var umbracoMedia = _mediaService.CreateMedia(filename, _productsMediaFolder, UmbracoModels.Image.ModelTypeAlias);

			using (fileStream)
			{
				umbracoMedia.SetValue(_mediaFileManager, _mediaUrlGenerators, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, filename, fileStream, null,null);

				var result = _mediaService.Save(umbracoMedia);

				if(!result.Success)
				{
					return null;
				}
				return umbracoMedia.GetUdi();
			}
		}

		public Product Update(int id, ProductUpdateItem product)
		{
			var productContent = _contentService.GetById(id);
			if(!string.IsNullOrEmpty(product.ProductName))
			{
				productContent.SetValue(_productNameAlias, product.ProductName);
			}
			if(product.Price != null)
			{
				productContent.SetValue(_productPriceAlias, product.Price.Value);
			}
			if(product.Categories != null && product.Categories.Any())
			{
				productContent.SetValue(_productCategoriesAlias, string.Join(",", product.Categories));
			}
			if(!string.IsNullOrEmpty(product.Description))
			{
				productContent.SetValue(_productDescriptionAlias, product.Description);
			}
			if(!string.IsNullOrEmpty(product.SKU))
			{
				productContent.SetValue(_productSkuAlias, product.SKU);
			}
			if(!string.IsNullOrEmpty(product.PhotoFileName) && !string.IsNullOrEmpty(product.Photo))
			{
				var productImage = CreateProductImage(product.PhotoFileName, product.Photo);
				if(productImage != null)
				{
					productContent.SetValue(_productPhotoAlias, productImage);
				}
			}
			_contentService.SaveAndPublish(productContent);
			return Get(id);
		}
	}
}
