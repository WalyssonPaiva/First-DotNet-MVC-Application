using System.Collections.Generic;
using System.Linq;
using MVCStore.Domain.Interfaces;

namespace MVCStore.Domain.Notifications {
    public class Notificator : INotificator {
        private List<Notification> _notifications;

        public Notificator() {
            _notifications = new List<Notification>();
        }
        
        public bool HasNotification() {
            return _notifications.Any();
        }

        public List<Notification> GetNotification() {
            return _notifications;
        }

        public void Handle(Notification notification) {
            _notifications.Add(notification);
        }
    }
}