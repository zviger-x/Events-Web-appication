using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using FluentValidation;

namespace EventsManagement.BusinessLogic.Validation.Validators
{
    internal class UserValidator : BaseValidator<UserDTO>
    {
        public UserValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            RuleFor(u => u.Name)
            .NotNull().WithMessage(UserValidationMessages.NameNotNull)
            .NotEmpty().WithMessage(UserValidationMessages.NameNotEmpty);

            RuleFor(u => u.Surname)
                .NotNull().WithMessage(UserValidationMessages.SurnameNotNull)
                .NotEmpty().WithMessage(UserValidationMessages.SurnameNotEmpty);

            RuleFor(u => u.BirthDate)
                .NotNull().WithMessage(UserValidationMessages.BirthDateNotNull)
                .LessThan(DateTime.Now).WithMessage(UserValidationMessages.BirthDateInTheFuture);

            RuleFor(u => u.Email)
                .NotNull().WithMessage(UserValidationMessages.EmailNotNull)
                .NotEmpty().WithMessage(UserValidationMessages.EmailNotEmpty)
                .EmailAddress().WithMessage(UserValidationMessages.EmailInvalid);
        }
    }
}
