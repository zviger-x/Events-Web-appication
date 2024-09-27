using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserDeleteUseCase : BaseUseCase, IDeleteUseCase<User>
    {
        public UserDeleteUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task Delete(User entity)
        {
            await _unitOfWork.UserRepository.Delete(entity);
        }
    }
}
