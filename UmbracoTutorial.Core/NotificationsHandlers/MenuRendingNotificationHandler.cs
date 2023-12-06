using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace UmbracoTutorial.Core.NotificationsHandlers
{
    public class MenuRendingNotificationHandler : INotificationHandler<MenuRenderingNotification>
    {
        public void Handle(MenuRenderingNotification notification)
        {
            if (notification.TreeAlias.InvariantEquals("content") && notification.NodeId == "1102")
            {
                var item = notification.Menu.Items.FirstOrDefault(x => x.Alias.InvariantEquals("delete"));
                if (item != null)
                {
                    notification.Menu.Items.Remove(item);
                }
            }
        }
    }
}
