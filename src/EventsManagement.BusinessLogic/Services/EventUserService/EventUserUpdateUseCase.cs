using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUpdateUseCase : BaseUseCase, IUpdateUseCase<EventUserDTO>
    {
        public EventUserUpdateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task Update(EventUserDTO entity)
        {
            var e = _mapper.Map<EventUser>(entity);
            await _unitOfWork.EventUserRepository.Update(e);
        }
    }
}
