using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUnregisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IUnregisterUserInEventUseCase
    {
        public EventUserUnregisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task UnregisterUserInEventAsync(EventUserDTO eventUser)
        {
            // Главное, чтобы id совпадал
            // await _validator.ValidateAndThrowAsync(eventUser);

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
