using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Registers a user in an event.
    /// </summary>
    public interface IRegisterUserInEventUseCase : IUseCase<EventUserDTO, Task>
    {
    }
}
