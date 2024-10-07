using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IRegisterUserInEventUseCase
    {
        /// <summary>
        /// Registers a user in an event
        /// </summary>
        /// <param name="eventUser">User to register.</param>
        /// <returns></returns>
        Task RegisterUserInEventAsync(EventUserDTO eventUser);
    }
}
