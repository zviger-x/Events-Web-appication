using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces.EventUser;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserGetUsersOfEventUseCase : BaseUseCase<EventUserDTO>, IGetUsersOfEventUseCase
    {
        public EventUserGetUsersOfEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventUserDTO> Execute(int eventId)
        {
            if (eventId < 0)
                throw new ArgumentOutOfRangeException(nameof(eventId), StandartValidationMessages.ParameterIsLessThanZero);

            var users = _unitOfWork.EventUserRepository.GetUsersOfEvent(eventId);
            return users.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
