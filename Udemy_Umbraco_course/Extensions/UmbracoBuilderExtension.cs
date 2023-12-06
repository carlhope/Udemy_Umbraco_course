using Udemy_Umbraco_course.Mappings;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Notifications;
using UmbracoTutorial.Core.NotificationsHandlers;

namespace Udemy_Umbraco_course.Extensions
{
	public static class UmbracoBuilderExtension
	{
		public static IUmbracoBuilder AddContactRequestTable(this IUmbracoBuilder builder)
		{
			builder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunContactRequestMigration>();
			return builder;
		}

		public static IUmbracoBuilder AddContactRequestMappings(this IUmbracoBuilder builder)
		{
            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
                .Add<ContactRequestMapping>();
            return builder;
        }
	}
}
