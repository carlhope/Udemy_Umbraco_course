using Udemy_Umbraco_course.Routing;
using Umbraco.Cms.Core.Composing;

namespace Udemy_Umbraco_course.Composers
{
	public class ContentPagesComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.ContentFinders().Append<FindContentByOldUrl>();
		}
	}
}
