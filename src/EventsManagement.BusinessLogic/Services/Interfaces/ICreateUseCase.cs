using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface ICreateUseCase<T>
        where T : IEntity
    {
        /// <summary>
        /// Creates an entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        Task Create(T entity);
    }
}
