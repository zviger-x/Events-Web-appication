using EventsManagement.DataAccess.Repositories.Interfaces;
using EventsManagement.DataObjects.Entities;

namespace EventsManagement.BusinessLogic.UnitOfWork
{
    internal interface IUnitOfWork
    {
        IRepository<Event> EventRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<EventUser> EventUserRepository { get; }
    }
}
