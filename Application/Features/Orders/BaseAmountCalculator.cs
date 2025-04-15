using Domain.Entities;

namespace Application.Features.Orders
{
    public class BaseAmountCalculator : IAmountCalculator
    {
        public Task Calculate(Order order)
        {
            foreach (var item in order.Items!)
            {
                order.Amount += item.Price * item.Quantity;
            }
            return Task.CompletedTask;
        }
        
    }
}
