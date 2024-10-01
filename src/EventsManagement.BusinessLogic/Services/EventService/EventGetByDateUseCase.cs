using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByDateUseCase : BaseUseCase<EventDTO>, IGetEventByDateUseCase
    {
        public EventGetByDateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventDTO> GetByDate(DateTime date)
        {
            var events = _unitOfWork.EventRepository.GetByDate(date);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
