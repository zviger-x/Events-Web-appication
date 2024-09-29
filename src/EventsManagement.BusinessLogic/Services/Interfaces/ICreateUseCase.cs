using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface ICreateUseCase<T>
        where T : IEntityDTO
    {
        /// <summary>
        /// Creates an entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        Task CreateAsync(T entity);
    }
}
