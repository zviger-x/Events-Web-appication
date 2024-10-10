using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Returns an array of all entities
    /// </summary>
    /// <typeparam name="T">Entity data transfer object</typeparam>
    public interface IGetAllUseCase<T> : IUseCaseWithoutRequest<Task<IEnumerable<T>>>
        where T : IEntityDTO
    {
    }
}
