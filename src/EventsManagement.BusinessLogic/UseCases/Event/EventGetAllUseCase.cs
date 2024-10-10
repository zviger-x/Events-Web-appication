using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetAllUseCase : BaseUseCase<EventDTO>, IGetAllUseCase<EventDTO>
    {
        public EventGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventDTO>> Execute()
        {
            var events = await _unitOfWork.EventRepository.GetAll().ToListAsync();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return eventDTOs;
        }
    }
}
