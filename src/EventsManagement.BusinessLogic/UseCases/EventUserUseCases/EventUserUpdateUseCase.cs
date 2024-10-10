using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserUpdateUseCase : BaseUseCase<EventUserDTO>, IUpdateUseCase<EventUserDTO>
    {
        public EventUserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task Execute(EventUserDTO entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            var e = _mapper.Map<EventUser>(entity);
            _unitOfWork.EventUserRepository.Update(e);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
