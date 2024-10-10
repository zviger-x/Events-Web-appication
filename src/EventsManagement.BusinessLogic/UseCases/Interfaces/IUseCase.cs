namespace EventsManagement.BusinessLogic.UseCases.Interfaces
{
    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
