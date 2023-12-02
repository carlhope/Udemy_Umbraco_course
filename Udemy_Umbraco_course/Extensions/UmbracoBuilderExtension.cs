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
	}
}
