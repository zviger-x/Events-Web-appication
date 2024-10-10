using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetByUserIdAndEventIdUseCase : BaseUseCase<EventUserDTO>, IEventUserGetByUserIdAndEventIdUseCase
    {
        public EventUserGetByUserIdAndEventIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventUserDTO> GetByUserIdAndEventId(int userId, int eventId)
        {
            var eventUser = await _unitOfWork.EventUserRepository.GetByUserIdAndEventId(userId, eventId);
            return _mapper.Map<EventUserDTO>(eventUser);
        }
    }
}
