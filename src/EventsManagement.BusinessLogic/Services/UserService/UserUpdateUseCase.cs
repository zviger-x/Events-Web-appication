using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserUpdateUseCase : BaseUseCase, IUpdateUseCase<User>
    {
        public UserUpdateUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Update(User entity)
        {
            await _unitOfWork.UserRepository.Update(entity);
        }
    }
}
