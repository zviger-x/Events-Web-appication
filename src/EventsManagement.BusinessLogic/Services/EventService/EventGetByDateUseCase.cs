using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByDateUseCase : BaseUseCase, IGetEventByDateUseCase
    {
        public EventGetByDateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventDTO> GetByDate(DateTime date)
        {
            var events = _unitOfWork.EventRepository.GetByDate(date);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
