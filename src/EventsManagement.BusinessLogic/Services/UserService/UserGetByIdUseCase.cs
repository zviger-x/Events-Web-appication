using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetByIdUseCase : BaseUseCase<UserDTO>, IGetByIdUseCase<UserDTO>
    {
        public UserGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<UserDTO> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), StandartValidationMessages.ParameterIsLessThanZero);

            var u = await _unitOfWork.UserRepository.GetById(id);
            return _mapper.Map<UserDTO>(u);
        }
    }
}
