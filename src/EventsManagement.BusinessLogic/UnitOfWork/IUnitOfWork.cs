using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UnitOfWork
{
    internal interface IUnitOfWork
    {
        IEventRepository EventRepository { get; }
        IUserRepository UserRepository { get; }
        IEventUserRepository EventUserRepository { get; }
    }
}
