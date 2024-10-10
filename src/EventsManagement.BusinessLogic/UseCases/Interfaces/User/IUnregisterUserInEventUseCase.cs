namespace EventsManagement.BusinessLogic.UseCases.Interfaces.User
{
    /// <summary>
    /// Unregisters a user in an event.
    /// </summary>
    public interface IUnregisterUserInEventUseCase : IUseCase<(int? userId, int? eventId), Task>
    {
    }
}
