using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetUsersOfEventUseCase : BaseUseCase, IGetUsersOfEventUseCase
    {
        public EventUserGetUsersOfEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventUserDTO> GetUsersOfEvent(int eventId)
        {
            var users = _unitOfWork.EventUserRepository.GetUsersOfEvent(eventId);
            return users.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
