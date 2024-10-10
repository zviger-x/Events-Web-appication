namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IUseCaseWithoutRequest<out TResponse>
    {
        TResponse Execute();
    }
}
