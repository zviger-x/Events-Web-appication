using EventsManagement.BusinessLogic.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services
{
    internal abstract class BaseUseCase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}