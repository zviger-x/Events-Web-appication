using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetEventByVenueUseCase
    {
        /// <summary>
        /// Returns an array of events by venue.
        /// </summary>
        /// <param name="venue">Event venue.</param>
        /// <returns>An array of events.</returns>
        IQueryable<EventDTO> GetByVenue(string venue);
    }
}
