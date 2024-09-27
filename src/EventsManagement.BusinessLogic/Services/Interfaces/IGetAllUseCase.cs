using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetAllUseCase<T>
        where T : IEntity
    {

        /// <summary>
        /// Returns an array of all entities in the repository.
        /// </summary>
        /// <returns>An array of all entities in the repository.</returns>
        IQueryable<T> GetAll();
    }
}
