using EventsManagement.DataObjects.Entities.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetByIdUseCase<T>
        where T : IEntity
    {
        /// <summary>
        /// Returns an entity by its id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetById(int id);
    }
}
