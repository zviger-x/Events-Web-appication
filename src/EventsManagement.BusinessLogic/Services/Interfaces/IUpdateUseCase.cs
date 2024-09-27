using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IUpdateUseCase<T>
        where T : IEntity
    {
        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        Task Update(T entity);
    }
}
