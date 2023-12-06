using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;
using Umbraco.Extensions;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.NotificationsHandlers
{
    public class SendingContentNotificationHandler : INotificationHandler<SendingContentNotification>
    {
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        public SendingContentNotificationHandler(IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
        {
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        }
        public void Handle(SendingContentNotification notification)
        {
            var currentUser = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;

            // Umbraco actions
            //update/save = A
            //publish = U
            //unpublish = Z
            //create = C


            if(!currentUser.Groups.Any(x => x.Alias == Umbraco.Cms.Core.Constants.Security.AdminGroupAlias))
            {
                var actionsToRemove = new List<string> { "Z", "A" };

                notification.Content.AllowedActions = notification.Content.AllowedActions.Where(x => !actionsToRemove.Contains(x));
                notification.Content.AllowPreview = false;
            }

           SetDefaultValueForPublicationDate(notification);
        }

        private void SetDefaultValueForPublicationDate(SendingContentNotification notification)
        {
            if (notification.Content.ContentTypeAlias != Blogpost.ModelTypeAlias)
            {
                return;
            }

            foreach (var variant in notification.Content.Variants)
            {
                var puplishedDateProperty = variant.Tabs.SelectMany(f => f.Properties)
                        .FirstOrDefault(f => f.Alias.InvariantEquals("publicationDate"));

                if (variant.State != ContentSavedState.NotCreated)
                {
                    return;
                }

                if (puplishedDateProperty != null)
                {
                    puplishedDateProperty.Value = DateTime.Now;

                }
            }
        }
    }
}
