using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetEventsByDateUseCase
    {
        /// <summary>
        /// Returns an array of events by date.
        /// </summary>
        /// <param name="date">Event date and time.</param>
        /// <returns>An array of events.</returns>
        Task<IEnumerable<EventDTO>> GetByDateAsync(DateTime date);
    }
}
