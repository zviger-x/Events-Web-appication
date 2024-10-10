using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.BusinessLogic.UseCases.EventUserUseCases
{
    internal class EventUserGetAllUseCase : BaseUseCase<EventUserDTO>, IGetAllUseCase<EventUserDTO>
    {
        public EventUserGetAllUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<IEnumerable<EventUserDTO>> Execute()
        {
            var eventUsers = await _unitOfWork.EventUserRepository.GetAll().ToListAsync();
            var eventUsersDTOs = _mapper.Map<IEnumerable<EventUserDTO>>(eventUsers);
            return eventUsersDTOs;
        }
    }
}
