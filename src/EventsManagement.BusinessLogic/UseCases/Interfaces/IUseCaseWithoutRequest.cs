namespace EventsManagement.BusinessLogic.UseCases.Interfaces
{
    public interface IUseCaseWithoutRequest<out TResponse>
    {
        TResponse Execute();
    }
}
