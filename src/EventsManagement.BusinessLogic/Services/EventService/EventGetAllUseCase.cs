using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetAllUseCase : BaseUseCase, IGetAllUseCase<EventDTO>
    {
        public EventGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
