using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetByIdUseCase : BaseUseCase, IGetByIdUseCase<User>
    {
        public UserGetByIdUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }
    }
}
