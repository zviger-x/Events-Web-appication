// На неопределённый срок функционал отключен 
//
// using AutoMapper;
// using EventsManagement.BusinessLogic.DataTransferObjects;
// using EventsManagement.BusinessLogic.Services.Interfaces;
// using EventsManagement.BusinessLogic.UnitOfWork;
// using EventsManagement.DataObjects.Entities;
// 
// namespace EventsManagement.BusinessLogic.Services.EventUserService
// {
//     internal class EventUserDeleteUseCase : BaseUseCase, IDeleteUseCase<EventUserDTO>
//     {
//         public EventUserDeleteUseCase(IUnitOfWork unitOfWork, IMapper mapper)
//             : base(unitOfWork, mapper)
//         {
//         }
// 
//         public async Task Delete(EventUserDTO entity)
//         {
//             var e = _mapper.Map<EventUser>(entity);
//             await _unitOfWork.EventUserRepository.Delete(e);
//         }
//     }
// }
