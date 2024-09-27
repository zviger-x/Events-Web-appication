using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserCreateUseCase : BaseUseCase, ICreateUseCase<User>
    {
        public UserCreateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Create(User entity)
        {
            await _unitOfWork.UserRepository.Create(entity);
        }
    }
}
