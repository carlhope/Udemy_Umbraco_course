using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using UmbracoTutorial.Core.Models;

namespace UmbracoTutorial.Core.PropertyValueConverters
{
    public class GithubUserPropertyValueConverter: PropertyValueConverterBase
    {
        public override bool IsConverter(IPublishedPropertyType propertyType) =>
            propertyType.EditorAlias.Equals("githubUser");

        public override object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? source, bool preview)
        {
            if(source == null)
            {
                return null;
            }

            return System.Text.Json.JsonSerializer.Deserialize<GithubUserDTO>((string)source);
        }

    }
}
