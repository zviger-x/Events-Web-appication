using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetEventByDateUseCase
    {
        /// <summary>
        /// Returns an array of events by date.
        /// </summary>
        /// <param name="date">Event date and time.</param>
        /// <returns>An array of events.</returns>
        IQueryable<EventDTO> GetByDate(DateTime date);
    }
}
