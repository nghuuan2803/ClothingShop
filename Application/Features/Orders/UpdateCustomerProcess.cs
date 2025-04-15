using Domain.Entities;
using Domain.Repositories;

namespace Application.Features.Orders
{
    public class UpdateCustomerProcess
    {
        private readonly IRepository<Customer> _customerRepository;

        public UpdateCustomerProcess(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task Excute(Customer customer, Order order)
        {
            customer.PhoneNumber = customer.PhoneNumber ?? order.PhoneNumber;
            customer.Name = string.IsNullOrEmpty(customer.Name) ? order.CustomerName : customer.Name;
            _customerRepository.Update(customer);

            return Task.CompletedTask;
        }
    }
}
