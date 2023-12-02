using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace UmbracoTutorial.Core.NotificationsHandlers
{
    public class ContentPublishingNotificationHandler : INotificationHandler<ContentPublishingNotification>
    {
        ILogger<ContentPublishingNotificationHandler> _logger;
        public ContentPublishingNotificationHandler(ILogger<ContentPublishingNotificationHandler> logger)
        {
            _logger = logger;
        }
        public void Handle(ContentPublishingNotification notification)
        {
            var publishedEntities = notification.PublishedEntities;
            foreach(var entity in publishedEntities)
            {
                _logger.LogInformation("Publishing node with id: {nodeId}", entity.Id);        
            }   
           
        }
    }
}
