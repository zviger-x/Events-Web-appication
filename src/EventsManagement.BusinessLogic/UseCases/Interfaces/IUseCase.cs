namespace EventsManagement.BusinessLogic.Services.Interfaces
{
    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
