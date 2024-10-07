namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IEventUserCheckRegistrationUseCase
    {
        Task<bool> IsUserRegisteredAsync(int userId, int eventId);
    }
}
