using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByVenueUseCase : BaseUseCase<EventDTO>, IGetEventsByVenueUseCase
    {
        public EventGetByVenueUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventDTO>> GetByVenueAsync(string venue)
        {
            if (string.IsNullOrEmpty(venue))
                throw new ArgumentNullException(nameof(venue), StandartValidationMessages.ParameterIsNullOrEmpty);

            var events = await _unitOfWork.EventRepository.GetByVenue(venue).ToListAsync();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return eventDTOs;
        }
    }
}
