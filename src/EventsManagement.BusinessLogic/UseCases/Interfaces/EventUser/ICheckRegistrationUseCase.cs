using EventsManagement.BusinessLogic.Services.Interfaces;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser
{
    /// <summary>
    /// Checks if the user is registered for the event
    /// </summary>
    public interface ICheckRegistrationUseCase : IUseCase<(int userId, int eventId), Task<bool>>
    {
    }
}
