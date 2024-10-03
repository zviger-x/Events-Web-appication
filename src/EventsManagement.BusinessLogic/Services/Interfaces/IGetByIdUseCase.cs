using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetByIdUseCase<T>
        where T : IEntityDTO
    {
        /// <summary>
        /// Returns an entity by its id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetByIdAsync(int id);
    }
}
