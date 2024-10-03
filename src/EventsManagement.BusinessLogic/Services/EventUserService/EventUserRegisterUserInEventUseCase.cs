using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserRegisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IRegisterUserInEventUseCase
    {
        public EventUserRegisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task RegisterUserInEventAsync(EventUserDTO eventUser)
        {
            await _validator.ValidateAndThrowAsync(eventUser);

            var eu = _mapper.Map<EventUser>(eventUser);
            await _unitOfWork.EventUserRepository.RegisterUserInEventAsync(eu);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
