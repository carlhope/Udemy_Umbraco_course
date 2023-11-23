using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoTutorial.ViewModels
{
    public class ProductListingBlockListViewModel
    {
        public ProductListingBlockListViewModel(IPublishedContent content)
        {
            Content = content;
        }

        public IPublishedContent Content { get; set; }

        public string ProductName { get; set; }
        
    }
}