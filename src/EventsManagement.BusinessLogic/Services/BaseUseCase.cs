using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataAccess.UnitOfWork;

namespace EventsManagement.BusinessLogic.Services
{
    internal abstract class BaseUseCase<T>
        where T : IEntityDTO
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IBaseValidator<T> _validator;

        protected BaseUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<T> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
    }
}