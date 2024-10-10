using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserGetByUserIdAndEventIdUseCase : BaseUseCase<EventUserDTO>, IEventUserGetByUserIdAndEventIdUseCase
    {
        public EventUserGetByUserIdAndEventIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventUserDTO> Execute((int userId, int eventId) request)
        {
            var eventUser = await _unitOfWork.EventUserRepository.GetByUserIdAndEventId(request.userId, request.eventId);
            return _mapper.Map<EventUserDTO>(eventUser);
        }
    }
}
