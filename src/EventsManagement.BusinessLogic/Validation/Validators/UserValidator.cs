using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.DataAccess.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
                .EmailAddress().WithMessage(UserValidationMessages.EmailInvalid)
                .MustAsync(IsUniqueEmail).WithMessage(UserValidationMessages.EmailMustBeUnique);
        }

        private async Task<bool> IsUniqueEmail(UserDTO user, string email, CancellationToken token)
        {
            if (user.IsUpdate)
            {
                return !await _unitOfWork.UserRepository.GetAll()
                    .AnyAsync(e => e.Email == email && e.Id != user.Id, token);
            }

            return !await _unitOfWork.UserRepository.GetAll()
                .AnyAsync(e => e.Email == email, token);
        }
    }
}
