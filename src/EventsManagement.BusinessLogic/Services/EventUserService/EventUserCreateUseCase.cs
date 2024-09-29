// На неопределённый срок функционал отключен (Реализация перенесена в RegisterUser)
//
// using AutoMapper;
// using EventsManagement.BusinessLogic.DataTransferObjects;
// using EventsManagement.BusinessLogic.Services.Interfaces;
// using EventsManagement.BusinessLogic.UnitOfWork;
// using EventsManagement.DataObjects.Entities;
// 
// namespace EventsManagement.BusinessLogic.Services.EventUserService
// {
//     internal class EventUserCreateUseCase : BaseUseCase, ICreateUseCase<EventUserDTO>
//     {
//         public EventUserCreateUseCase(IUnitOfWork unitOfWork, IMapper mapper)
//             : base(unitOfWork, mapper)
//         {
//         }
// 
//         public async Task Create(EventUserDTO entity)
//         {
//             var e = _mapper.Map<EventUser>(entity);
//             await _unitOfWork.EventUserRepository.Create(e);
//         }
//     }
// }
