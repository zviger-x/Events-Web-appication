using EventsManagement.BusinessLogic.DataTransferObjects;

namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IEventUserGetByUserIdAndEventIdUseCase
    {
        Task<EventUserDTO> GetByUserIdAndEventId(int userId, int eventId);
    }
}
