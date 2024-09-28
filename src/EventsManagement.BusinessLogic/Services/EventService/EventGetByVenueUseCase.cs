using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByVenueUseCase : BaseUseCase, IGetEventByVenueUseCase
    {
        public EventGetByVenueUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventDTO> GetByVenue(string venue)
        {
            var events = _unitOfWork.EventRepository.GetByVenue(venue);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
