using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Validation.Validators
{
    internal class EventUserValidator : BaseValidator<EventUserDTO>
    {
        public EventUserValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            RuleFor(eu => eu.UserId)
                .NotNull().WithMessage(EventUserValidationMessages.UserIdNotNull)
                .GreaterThan(0).WithMessage(EventUserValidationMessages.UserIdInvalid)
                .MustAsync(IsUserExists).WithMessage(EventUserValidationMessages.UserNotFound);

            RuleFor(eu => eu.EventId)
                .NotNull().WithMessage(EventUserValidationMessages.EventIdNotNull)
                .GreaterThan(0).WithMessage(EventUserValidationMessages.EventIdInvalid)
                .MustAsync(IsEventExists).WithMessage(EventUserValidationMessages.EventNotFound)
                .MustAsync(IsEventHasSpace).WithMessage(EventUserValidationMessages.EventMaxParticipantsReached);

            RuleFor(eu => eu.RegistrationDate)
                .NotNull().WithMessage(EventUserValidationMessages.RegistrationDateNotNull)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(EventUserValidationMessages.RegistrationDateInTheFuture);
        }

        private async Task<bool> IsUserExists(int userId, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            return user != null;
        }


        private async Task<bool> IsEventExists(int eventId, CancellationToken token)
        {
            var eventEntity = await _unitOfWork.EventRepository.GetByIdAsync(eventId);
            return eventEntity != null;
        }


        private async Task<bool> IsEventHasSpace(int eventId, CancellationToken token)
        {
            var eventEntity = await _unitOfWork.EventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                return false;
            }

            var participantsCount = await _unitOfWork.EventUserRepository.GetUsersOfEventAsync(eventId).CountAsync(token);
            return participantsCount < eventEntity.MaxNumberOfParticipants;
        }
    }
}
