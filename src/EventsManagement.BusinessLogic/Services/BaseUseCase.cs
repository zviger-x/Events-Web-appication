using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services
{
    internal abstract class BaseUseCase<T>
        where T : IEntityDTO
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly BaseValidator<T> _validator;

        protected BaseUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<T> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
    }
}