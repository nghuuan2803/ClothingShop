﻿using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record AddGuestCommand(string id) : IRequest<string> { }

    public class AddGuestCommandHandler : IRequestHandler<AddGuestCommand, string>
    {
        private readonly IRepository<Customer> _customerRepo;

        public AddGuestCommandHandler(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<string> Handle(AddGuestCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.id
            };
            await _customerRepo.AddAsync(customer);
            await _customerRepo.SaveChangesAsync();
            return "Guest is added succesfully!";
        }
    }
}
