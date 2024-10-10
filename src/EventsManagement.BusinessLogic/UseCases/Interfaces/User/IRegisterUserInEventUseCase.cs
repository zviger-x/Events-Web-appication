using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Registers a user in an event.
    /// </summary>
    public interface IRegisterUserInEventUseCase : IUseCase<(int? userId, int? eventId), Task>
    {
    }
}
