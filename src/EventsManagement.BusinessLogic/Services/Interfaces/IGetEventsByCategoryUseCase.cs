using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IGetEventsByCategoryUseCase
    {
        /// <summary>
        /// Returns an array of events by category.
        /// </summary>
        /// <param name="category">Event category.</param>
        /// <returns>An array of events.</returns>
        Task<IEnumerable<EventDTO>> GetByCategoryAsync(string category);
    }
}
