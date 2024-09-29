using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Validation.Validators
{

    internal class EventValidator : BaseValidator<EventDTO>
    {
        public EventValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            RuleFor(e => e.Name)
                .NotNull().WithMessage(EventValidationMessages.NameNotNull)
                .NotEmpty().WithMessage(EventValidationMessages.NameNotEmpty)
                .MustAsync(IsUniqueName).WithMessage(EventValidationMessages.NameMustBeUnique);

            RuleFor(e => e.Description)
                .NotNull().WithMessage(EventValidationMessages.DescriptionNotNull)
                .NotEmpty().WithMessage(EventValidationMessages.DescriptionNotEmpty);

            RuleFor(e => e.DateAndTime)
                .NotNull().WithMessage(EventValidationMessages.DateAndTimeNotNull);

            RuleFor(e => e.MaxNumberOfParticipants)
                .GreaterThan(0).WithMessage(EventValidationMessages.MaxParticipantsGreaterThanZero);
        }

        private async Task<bool> IsUniqueName(string name, CancellationToken token)
        {
            return !await _unitOfWork.EventRepository.GetAllAsync().AnyAsync(e => e.Name == name, token);
        }
    }
}
