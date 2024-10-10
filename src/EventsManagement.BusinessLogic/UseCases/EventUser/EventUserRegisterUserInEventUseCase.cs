using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.UseCases.Interfaces.User;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserRegisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IRegisterUserInEventUseCase
    {
        private readonly ICheckRegistrationUseCase _checkRegistrationUseCase;

        public EventUserRegisterUserInEventUseCase(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBaseValidator<EventUserDTO> validator,
            ICheckRegistrationUseCase checkRegistrationUse)
            : base(unitOfWork, mapper, validator)
        {
            _checkRegistrationUseCase = checkRegistrationUse;
        }

        public async Task Execute((int? userId, int? eventId) request)
        {
            if (request.userId is null)
                throw new ArgumentNullException(nameof(request.userId));
            if (request.eventId is null)
                throw new ArgumentNullException(nameof(request.eventId));

            var isRegistered = await _checkRegistrationUseCase.Execute((request.userId.Value, request.eventId.Value));
            if (isRegistered)
            {
                throw new ArgumentException("User is already registered for this event.");
            }

            var eventUser = new EventUserDTO
            {
                UserId = request.userId.Value,
                EventId = request.eventId.Value,
                RegistrationDate = DateTime.UtcNow
            };
            await _validator.ValidateAndThrowAsync(eventUser);

            var eu = _mapper.Map<EventUser>(eventUser);
            await _unitOfWork.EventUserRepository.RegisterUserInEventAsync(eu);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
