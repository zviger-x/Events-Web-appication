using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.EventUseCases
{
    internal class EventGetByIdUseCase : BaseUseCase<EventDTO>, IGetByIdUseCase<EventDTO>
    {
        public EventGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventDTO> Execute(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), StandartValidationMessages.ParameterIsLessThanZero);

            var e = await _unitOfWork.EventRepository.GetByIdAsync(id);
            return _mapper.Map<EventDTO>(e);
        }
    }
}
