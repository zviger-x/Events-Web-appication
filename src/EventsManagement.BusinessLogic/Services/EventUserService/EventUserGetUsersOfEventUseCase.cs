using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetUsersOfEventUseCase : BaseUseCase<EventUserDTO>, IGetUsersOfEventUseCase
    {
        public EventUserGetUsersOfEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventUserDTO> GetUsersOfEvent(int eventId)
        {
            if (eventId < 0)
                throw new ArgumentOutOfRangeException(nameof(eventId), StandartValidationMessages.ParameterIsLessThanZero);

            var users = _unitOfWork.EventUserRepository.GetUsersOfEvent(eventId);
            return users.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
