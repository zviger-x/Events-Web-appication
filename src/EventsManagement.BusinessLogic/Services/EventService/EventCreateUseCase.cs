using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventCreateUseCase : BaseUseCase<EventDTO>, ICreateUseCase<EventDTO>
    {
        public EventCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task CreateAsync(EventDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<Event>(entity);
            await _unitOfWork.EventRepository.CreateAsync(e);
            await _unitOfWork.EventRepository.SaveChangesAsync();
        }
    }
}
