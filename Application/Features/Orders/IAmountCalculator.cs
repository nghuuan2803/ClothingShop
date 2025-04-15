using Domain.Entities;

namespace Application.Features.Orders
{
    public interface IAmountCalculator
    {
        Task Calculate(Order order);
    }
}
