using AutoMapper;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserRegisterUserInEventUseCase : BaseUseCase, IRegisterUserInEventUseCase
    {
        public EventUserRegisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task RegisterUserInEvent(int userId, int eventId, DateTime registrationDate)
        {
            await _unitOfWork.EventUserRepository.RegisterUserInEvent(userId, eventId, registrationDate);
        }
    }
}
