using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserUnregisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IUnregisterUserInEventUseCase
    {
        private readonly ICheckRegistrationUseCase _checkRegistrationUseCase;
        private readonly IEventUserGetByUserIdAndEventIdUseCase _eventUserGetByUserIdAndEventIdUseCase;

        public EventUserUnregisterUserInEventUseCase(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBaseValidator<EventUserDTO> validator,
            ICheckRegistrationUseCase checkRegistrationUseCase,
            IEventUserGetByUserIdAndEventIdUseCase eventUserGetByUserIdAndEventIdUseCase)
            : base(unitOfWork, mapper, validator)
        {
            _checkRegistrationUseCase = checkRegistrationUseCase;
            _eventUserGetByUserIdAndEventIdUseCase = eventUserGetByUserIdAndEventIdUseCase;
        }

        public async Task Execute((int? userId, int? eventId) request)
        {
            if (request.userId is null)
                throw new ArgumentNullException(nameof(request.userId));
            if (request.eventId is null)
                throw new ArgumentNullException(nameof(request.eventId));

            var eventUser = await _eventUserGetByUserIdAndEventIdUseCase.Execute((request.userId.Value, request.eventId.Value));
            if (eventUser == null)
            {
                throw new ArgumentNullException("User is not registered for this event.");
            }

            // Фикс проблемы отслеживания объекта EF
            var existingEventUser = await _unitOfWork.EventUserRepository.GetByIdAsync(eventUser.Id);
            if (existingEventUser == null)
                return;

            var eu = _mapper.Map<EventUser>(existingEventUser);
            _unitOfWork.EventUserRepository.UnregisterUserInEvent(eu);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
