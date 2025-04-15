using Domain.Entities;

namespace Application.Features.Orders
{
    public interface IShipFeeCalculator
    {
        Task CalculateFee(Order order);
    }
}
