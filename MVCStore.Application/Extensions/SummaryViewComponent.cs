using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCStore.Domain.Interfaces;

namespace MVCStore.Application.Extensions {
    public class SummaryViewComponent : ViewComponent {
        private readonly INotificator _notificator;

        public SummaryViewComponent(INotificator notificator) {
            _notificator = notificator;
        }
        
        public async Task<IViewComponentResult> InvokeAsync() {
            var notifications = await Task.FromResult(_notificator.GetNotification());
            notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));
            return View();
        }
        
    }
}