using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUnregisterUserInEventUseCase : BaseUseCase, IUnregisterUserInEventUseCase
    {
        public EventUserUnregisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task UnregisterUserInEvent(EventUserDTO eventUser)
        {
            var eu = _mapper.Map<EventUser>(eventUser);
            await _unitOfWork.EventUserRepository.UnregisterUserInEvent(eu);
        }
    }
}
