using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    internal interface IGetEventByCategoryUseCase
    {
        /// <summary>
        /// Returns an array of events by category.
        /// </summary>
        /// <param name="category">Event category.</param>
        /// <returns>An array of events.</returns>
        IQueryable<EventDTO> GetByCategoryAsync(string category);
    }
}
