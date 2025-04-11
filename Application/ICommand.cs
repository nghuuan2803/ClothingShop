namespace Application
{
    public interface ICommand<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}
