using Microsoft.AspNetCore.Mvc;
using MVCStore.Domain.Interfaces;

namespace MVCStore.Application.Controllers {
    public class BaseController : Controller {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator) {
            _notificator = notificator;
        }

        protected bool ValidOperation() {
            return !_notificator.HasNotification();
        }
    }
}