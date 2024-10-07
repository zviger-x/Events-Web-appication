using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserGetAllUseCase : BaseUseCase<EventUserDTO>, IGetAllUseCase<EventUserDTO>
    {
        public EventUserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventUserDTO>> GetAllAsync()
        {
            var eventUsers = await _unitOfWork.EventUserRepository.GetAll().ToListAsync();
            var eventUsersDTOs = _mapper.Map<IEnumerable<EventUserDTO>>(eventUsers);
            return eventUsersDTOs;
        }
    }
}
