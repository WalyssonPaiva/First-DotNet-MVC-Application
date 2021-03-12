using FluentValidation;
using FluentValidation.Results;
using MVCStore.Domain.Entities;
using MVCStore.Domain.Interfaces;
using MVCStore.Domain.Notifications;

namespace MVCStore.Domain.Services {
    public abstract class BaseService {

        private readonly INotificator _notificator;

        public BaseService(INotificator notificator) {
            _notificator = notificator;
        }
        
        protected void Notify(string message) {
            _notificator.Handle(new Notification(message));
        }
        protected void Notify(ValidationResult validationResult) {
            foreach (var error in validationResult.Errors) {
                Notify(error.ErrorMessage);
            }
        }

        protected bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;
            
            Notify(validator);

            return false;
        }
    }
}