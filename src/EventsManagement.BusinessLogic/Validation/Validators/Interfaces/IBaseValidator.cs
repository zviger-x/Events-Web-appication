using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;
using FluentValidation;

namespace EventsManagement.BusinessLogic.Validation.Validators.Interfaces
{
    internal interface IBaseValidator<T> : IValidator<T>
        where T : IEntityDTO
    {
        Task ValidateAndThrowAsync(T entity);
    }
}
