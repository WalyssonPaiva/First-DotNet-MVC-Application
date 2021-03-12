using System.Collections.Generic;
using MVCStore.Domain.Notifications;

namespace MVCStore.Domain.Interfaces {
    public interface INotificator {
        bool HasNotification();

        List<Notification> GetNotification();

        void Handle(Notification notification);
    }
}