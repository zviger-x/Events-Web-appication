using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUpdateUseCase : BaseUseCase<EventUserDTO>, IUpdateUseCase<EventUserDTO>
    {
        public EventUserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task UpdateAsync(EventUserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<EventUser>(entity);
            _unitOfWork.EventUserRepository.Update(e);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
