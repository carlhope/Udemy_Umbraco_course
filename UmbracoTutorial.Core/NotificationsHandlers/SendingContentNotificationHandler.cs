using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using UmbracoTutorial.Core.UmbracoModels;

namespace UmbracoTutorial.Core.NotificationsHandlers
{
    public class SendingContentNotificationHandler : INotificationHandler<SendingContentNotification>
    {
        public void Handle(SendingContentNotification notification)
        {
            if(notification.Content.ContentTypeAlias != Blogpost.ModelTypeAlias)
            {
                return;
            }

            foreach(var variant in notification.Content.Variants)
            {
                var puplishedDateProperty = variant.Tabs.SelectMany(f => f.Properties)
                        .FirstOrDefault(f => f.Alias.InvariantEquals("publicationDate"));

                if(variant.State != ContentSavedState.NotCreated)
                {
                    return;
                }

                if ( puplishedDateProperty != null)
                {
                    puplishedDateProperty.Value = DateTime.Now;

                }
            }
        }
    }
}
