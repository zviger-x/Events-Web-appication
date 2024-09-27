using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IDeleteUseCase<T>
        where T : IEntity
    {
        /// <summary>
        /// Removes an entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        Task Delete(T entity);
    }
}
