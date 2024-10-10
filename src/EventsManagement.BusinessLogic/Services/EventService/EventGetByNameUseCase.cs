using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByNameUseCase : BaseUseCase<EventDTO>, IGetEventByNameUseCase
    {
        public EventGetByNameUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventDTO> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), StandartValidationMessages.ParameterIsNullOrEmpty);

            var e = await _unitOfWork.EventRepository.GetByNameAsync(name);
            return _mapper.Map<EventDTO>(e);
        }
    }
}
