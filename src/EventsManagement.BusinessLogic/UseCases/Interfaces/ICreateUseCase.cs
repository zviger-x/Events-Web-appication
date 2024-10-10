using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <typeparam name="T">Entity data transfer object</typeparam>
    public interface ICreateUseCase<T> : IUseCase<T, Task>
        where T : IEntityDTO
    {
    }
}
