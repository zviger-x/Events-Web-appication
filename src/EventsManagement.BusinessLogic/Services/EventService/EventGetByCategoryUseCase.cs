using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByCategoryUseCase : BaseUseCase<EventDTO>, IGetEventByCategoryUseCase
    {
        public EventGetByCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventDTO> GetByCategoryAsync(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException(nameof(category), StandartValidationMessages.ParameterIsNullOrEmpty);

            var events = _unitOfWork.EventRepository.GetByCategoryAsync(category);
            return events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider);
        }
    }
}
