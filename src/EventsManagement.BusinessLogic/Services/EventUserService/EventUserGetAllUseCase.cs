using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetAllUseCase : BaseUseCase<EventUserDTO>, IGetAllUseCase<EventUserDTO>
    {
        public EventUserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public IQueryable<EventUserDTO> GetAllAsync()
        {
            var eventUsers = _unitOfWork.EventUserRepository.GetAllAsync();
            return eventUsers.ProjectTo<EventUserDTO>(_mapper.ConfigurationProvider);
        }
    }
}
