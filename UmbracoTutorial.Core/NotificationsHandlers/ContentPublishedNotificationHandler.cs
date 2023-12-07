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
    public class ContentPublishedNotificationHandler : INotificationHandler<ContentPublishedNotification>
    {
        ILogger<ContentPublishedNotificationHandler> _logger;
        public ContentPublishedNotificationHandler(ILogger<ContentPublishedNotificationHandler> logger)
        {
            _logger = logger;
        }
        public void Handle(ContentPublishedNotification notification)
        {
            var publishedEntities = notification.PublishedEntities;
            foreach(var entity in publishedEntities)
            {
                _logger.LogInformation("Published node with id: {nodeId}", entity.Id);        
            }
        }
    }
}
