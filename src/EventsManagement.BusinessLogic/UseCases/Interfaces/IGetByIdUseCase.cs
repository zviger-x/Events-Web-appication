using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces
{
    /// <summary>
    /// Returns an entity by its id.
    /// </summary>
    /// <typeparam name="T">Entity data transfer object</typeparam>
    public interface IGetByIdUseCase<T> : IUseCase<int, Task<T>>
        where T : IEntityDTO
    {
    }
}
