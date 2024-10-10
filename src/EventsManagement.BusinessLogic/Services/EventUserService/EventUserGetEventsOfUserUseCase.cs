using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetEventsOfUserUseCase : BaseUseCase<EventUserDTO>, IGetEventsOfUserUseCase
    {
        public EventUserGetEventsOfUserUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IEnumerable<EventDTO> GetEventsOfUser(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId), StandartValidationMessages.ParameterIsLessThanZero);
            
            var eventsUser = _unitOfWork.EventUserRepository.GetEventsOfUser(userId)
                .Select(eu => eu.EventId)
                .ToList();

            var userEvents = _unitOfWork.EventRepository.GetAll()
                .Where(e => eventsUser.Contains(e.Id))
                .ToList();

            var outEvents = _mapper.Map<IEnumerable<EventDTO>>(userEvents);
            return outEvents;
        }
    }
}
