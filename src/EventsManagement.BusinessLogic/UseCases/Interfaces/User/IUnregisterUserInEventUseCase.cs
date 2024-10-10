using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Unregisters a user in an event.
    /// </summary>
    public interface IUnregisterUserInEventUseCase : IUseCase<EventUserDTO, Task>
    {
    }
}
