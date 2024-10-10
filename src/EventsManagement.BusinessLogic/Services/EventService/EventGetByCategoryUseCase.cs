using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByCategoryUseCase : BaseUseCase<EventDTO>, IGetEventsByCategoryUseCase
    {
        public EventGetByCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventDTO>> GetByCategoryAsync(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException(nameof(category), StandartValidationMessages.ParameterIsNullOrEmpty);

            var events = await _unitOfWork.EventRepository.GetByCategory(category).ToListAsync();
            var eventDTOs = _mapper.Map<IEnumerable<EventDTO>>(events);
            return eventDTOs;
        }
    }
}
