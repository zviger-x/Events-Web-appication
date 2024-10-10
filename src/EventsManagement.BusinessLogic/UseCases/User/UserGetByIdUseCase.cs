using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.UseCases.UserUseCases
{
    internal class UserGetByIdUseCase : BaseUseCase<UserDTO>, IGetByIdUseCase<UserDTO>
    {
        public UserGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<UserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<UserDTO> Execute(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), StandartValidationMessages.ParameterIsLessThanZero);

            var u = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(u);
        }
    }
}
