﻿using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Messages;
using EventsManagement.BusinessLogic.Validation.Validators;

namespace EventsManagement.BusinessLogic.Services.EventService
{
    internal class EventGetByIdUseCase : BaseUseCase<EventDTO>, IGetByIdUseCase<EventDTO>
    {
        public EventGetByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, BaseValidator<EventDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task<EventDTO> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), StandartValidationMessages.ParameterIsLessThanZero);

            var e = await _unitOfWork.EventRepository.GetById(id);
            return _mapper.Map<EventDTO>(e);
        }
    }
}
