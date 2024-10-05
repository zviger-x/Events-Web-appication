﻿using AutoMapper;
using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.Services.Interfaces;
using EventsManagement.BusinessLogic.UnitOfWork;
using EventsManagement.BusinessLogic.Validation.Validators.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.Services.EventUserService
{
    internal class EventUserUnregisterUserInEventUseCase : BaseUseCase<EventUserDTO>, IUnregisterUserInEventUseCase
    {
        public EventUserUnregisterUserInEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IBaseValidator<EventUserDTO> validator)
            : base(unitOfWork, mapper, validator)
        {
        }

        public async Task UnregisterUserInEventAsync(EventUserDTO eventUser)
        {
            // Главное, чтобы id совпадал
            // await _validator.ValidateAndThrowAsync(eventUser);

            var eu = _mapper.Map<EventUser>(eventUser);
            _unitOfWork.EventUserRepository.UnregisterUserInEvent(eu);
            await _unitOfWork.EventUserRepository.SaveChangesAsync();
        }
    }
}
