using UmbracoTutorial.Core.Models.Records;

namespace Udemy_Umbraco_course.ViewModels
{
    public class ProductListingViewModel
    {
        public List<ProductResponseItem> Products { get; set; } = new List<ProductResponseItem>();
    }
}
