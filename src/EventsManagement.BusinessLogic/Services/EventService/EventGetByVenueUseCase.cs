using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByVenueUseCase : BaseUseCase<EventDTO>, IGetEventByVenueUseCase
    {
        public EventGetByVenueUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventDTO> GetByVenue(string venue)
        {
            if (string.IsNullOrEmpty(venue))
                throw new ArgumentNullException(nameof(venue), StandartValidationMessages.ParameterIsNullOrEmpty);

            var events = _unitOfWork.EventRepository.GetByVenue(venue);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
