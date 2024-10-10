using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <typeparam name="T">Entity data transfer object</typeparam>
    public interface IUpdateUseCase<T> : IUseCase<T, Task>
        where T : IEntityDTO
    {
    }
}
