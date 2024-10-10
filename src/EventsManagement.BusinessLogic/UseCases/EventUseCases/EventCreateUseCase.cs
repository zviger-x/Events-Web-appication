using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UseCases.EventUseCases
{
    internal class EventCreateUseCase : BaseUseCase<EventDTO>, ICreateUseCase<EventDTO>
    {
        public EventCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task Execute(EventDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<Event>(entity);
            await _unitOfWork.EventRepository.CreateAsync(e);
            await _unitOfWork.EventRepository.SaveChangesAsync();
        }
    }
}
