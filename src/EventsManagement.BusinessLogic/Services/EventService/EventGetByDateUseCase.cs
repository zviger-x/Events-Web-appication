using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByDateUseCase : BaseUseCase<EventDTO>, IGetEventsByDateUseCase
    {
        public EventGetByDateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventDTO>> GetByDateAsync(DateTime date)
        {
            var events = await _unitOfWork.EventRepository.GetByDate(date).ToListAsync();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return eventDTOs;
        }
    }
}
