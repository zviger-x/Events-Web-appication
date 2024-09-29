using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IUnregisterUserInEventUseCase
    {
        /// <summary>
        /// Unregisters a user in an event
        /// </summary>
        /// <param name="eventUser">User of the event.</param>
        /// <returns></returns>
        Task UnregisterUserInEventAsync(EventUserDTO eventUser);
    }
}
