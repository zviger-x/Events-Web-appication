using EventsManagement.DataAccess.Repositories.Interfaces;

namespace EventsManagement.DataAccess.UnitOfWork
{
    internal interface IUnitOfWork
    {
        IEventRepository EventRepository { get; }
        IUserRepository UserRepository { get; }
        IEventUserRepository EventUserRepository { get; }
    }
}
