using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserCheckRegistrationUseCase : BaseUseCase<EventUserDTO>, ICheckRegistrationUseCase
    {
        public EventUserCheckRegistrationUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<bool> Execute((int userId, int eventId) request)
        {
            return await _unitOfWork.EventUserRepository.IsUserRegisteredAsync(request.userId, request.eventId);
        }
    }
}
