using Domain.Entities;

namespace Application.Features.Orders
{
    public class MockShipFeeCalculator : IShipFeeCalculator
    {
        public Task CalculateFee(Order order)
        {
            return Task.CompletedTask;
        }
    }
}
