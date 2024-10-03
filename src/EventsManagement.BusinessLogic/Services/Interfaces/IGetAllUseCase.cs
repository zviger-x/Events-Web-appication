using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetAllUseCase<T>
        where T : IEntityDTO
    {
        /// <summary>
        /// Returns an array of all entities in the repository.
        /// </summary>
        /// <returns>An array of all entities in the repository.</returns>
        IQueryable<T> GetAll();
    }
}
