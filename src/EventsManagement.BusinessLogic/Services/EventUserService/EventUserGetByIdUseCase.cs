using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetByIdUseCase : BaseUseCase<EventUserDTO>, IGetByIdUseCase<EventUserDTO>
    {
        public EventUserGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventUserDTO> GetByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), StandartValidationMessages.ParameterIsLessThanZero);

            var e = await _unitOfWork.EventUserRepository.GetByIdAsync(id);
            return _mapper.Map<EventUserDTO>(e);
        }
    }
}
