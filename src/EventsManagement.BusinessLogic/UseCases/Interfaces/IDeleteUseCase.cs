using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces
{
    /// <summary>
    /// Removes an entity.
    /// </summary>
    /// <typeparam name="T">Entity data transfer object</typeparam>
    public interface IDeleteUseCase<T> : IUseCase<T, Task>
        where T : IEntityDTO
    {
    }
}
