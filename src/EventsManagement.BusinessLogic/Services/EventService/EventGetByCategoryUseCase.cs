using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByCategoryUseCase : BaseUseCase, IGetEventByCategoryUseCase
    {
        public EventGetByCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IQueryable<EventDTO> GetByCategory(string category)
        {
            var events = _unitOfWork.EventRepository.GetByCategory(category);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
