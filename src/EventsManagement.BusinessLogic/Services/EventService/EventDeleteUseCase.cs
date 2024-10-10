using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventDeleteUseCase : BaseUseCase<EventDTO>, IDeleteUseCase<EventDTO>
    {
        public EventDeleteUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task DeleteAsync(EventDTO entity)
        {
            // Главное, чтобы id совпадал
            // await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<Event>(entity);
            _unitOfWork.EventRepository.Delete(e);
            await _unitOfWork.EventRepository.SaveChangesAsync();
        }
    }
}
