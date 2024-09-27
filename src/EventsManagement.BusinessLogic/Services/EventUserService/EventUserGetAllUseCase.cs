using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetAllUseCase : BaseUseCase, IGetAllUseCase<EventUserDTO>
    {
        public EventUserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventUserDTO> GetAll()
        {
            var eventUsers = _unitOfWork.EventUserRepository.GetAll();
            return eventUsers.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
