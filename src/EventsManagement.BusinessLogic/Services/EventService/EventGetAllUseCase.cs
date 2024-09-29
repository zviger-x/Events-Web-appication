using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetAllUseCase : BaseUseCase<EventDTO>, IGetAllUseCase<EventDTO>
    {
        public EventGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventDTO> GetAll()
        {
            var events = _unitOfWork.EventRepository.GetAll();
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
