using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using FluentValidation;

namespace EventsManagement.BusinessLogic.Validation.Validators
{
    internal class BaseValidator<T> : AbstractValidator<T>
        where T : IEntityDTO
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ValidateAndThrowAsync(T entity)
        {
            var result = await ValidateAsync(entity);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
