using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetEventByNameUseCase
    {
        /// <summary>
        /// Returns an event by its name.
        /// </summary>
        /// <param name="name">Event name.</param>
        /// <returns>Event.</returns>
        Task<EventDTO> GetByNameAsync(string name);
    }
}
