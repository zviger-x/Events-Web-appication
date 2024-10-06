using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetEventsByVenueUseCase
    {
        /// <summary>
        /// Returns an array of events by venue.
        /// </summary>
        /// <param name="venue">Event venue.</param>
        /// <returns>An array of events.</returns>
        Task<IEnumerable<EventDTO>> GetByVenueAsync(string venue);
    }
}
