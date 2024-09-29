using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUnregisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IUnregisterUserInEventUseCase
    {
        public EventUserUnregisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task UnregisterUserInEvent(EventUserDTO eventUser)
        {
            await _validator.ValidateAndThrowAsync(eventUser);

            var eu = _mapper.Map<EventUser>(eventUser);
            await _unitOfWork.EventUserRepository.UnregisterUserInEvent(eu);
        }
    }
}
