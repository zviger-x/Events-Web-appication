using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.UserService
{
    internal class UserGetAllUseCase : BaseUseCase, IGetAllUseCase<User>
    {
        public UserGetAllUseCase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }
    }
}
