using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Strings;

namespace Udemy_Umbraco_course.Routing
{
	public class ProductPageUrlSegmentProvider : IUrlSegmentProvider
	{
		private readonly IUrlSegmentProvider _provider;
		private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

		private readonly string _SKUAlias;

		public ProductPageUrlSegmentProvider(IShortStringHelper stringHelper, IPublishedSnapshotAccessor publishedSnapshotAccessor)
		{
			
			_provider = new DefaultUrlSegmentProvider(stringHelper);
			_publishedSnapshotAccessor = publishedSnapshotAccessor;
			_SKUAlias = UmbracoTutorial.Core.UmbracoModels.Product.GetModelPropertyType(_publishedSnapshotAccessor ,x => x.Sku).Alias;
		}
		public string? GetUrlSegment(IContentBase content, string? culture = null)
		{
			if (content.ContentType.Alias != UmbracoTutorial.Core.UmbracoModels.Product.ModelTypeAlias)
			{
				return null;
			}

			var currentSegment = _provider.GetUrlSegment(content, culture);
			var productSku = content.GetValue<string>(_SKUAlias).ToLower()??string.Empty;

			return !string.IsNullOrEmpty(productSku) ? $"{currentSegment}-{productSku}" : currentSegment;
		}
	}
}
