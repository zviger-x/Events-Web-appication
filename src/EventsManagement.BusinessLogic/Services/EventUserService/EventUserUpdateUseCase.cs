using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUpdateUseCase : BaseUseCase<EventUserDTO>, IUpdateUseCase<EventUserDTO>
    {
        public EventUserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task Update(EventUserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<EventUser>(entity);
            await _unitOfWork.EventUserRepository.Update(e);
        }
    }
}
