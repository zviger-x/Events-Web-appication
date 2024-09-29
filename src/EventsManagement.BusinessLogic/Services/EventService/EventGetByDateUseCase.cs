using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByDateUseCase : BaseUseCase<EventDTO>, IGetEventByDateUseCase
    {
        public EventGetByDateUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventDTO> GetByDateAsync(DateTime date)
        {
            var events = _unitOfWork.EventRepository.GetByDateAsync(date);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
