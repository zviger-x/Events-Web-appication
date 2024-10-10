using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserCheckRegistrationUseCase : BaseUseCase<EventUserDTO>, IEventUserCheckRegistrationUseCase
    {
        public EventUserCheckRegistrationUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<bool> IsUserRegisteredAsync(int userId, int eventId)
        {
            return await _unitOfWork.EventUserRepository.IsUserRegisteredAsync(userId, eventId);
        }
    }
}
