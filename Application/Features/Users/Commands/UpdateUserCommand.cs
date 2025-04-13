using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Users;

namespace Application.Features.Users.Commands
{
    //Cập nhật profile user
    public class UpdateUserCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public UpdateUserReq Request { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private IUserRepository _userRepo;
        private IRepository<Customer> _customerRepo;

        public UpdateUserCommandHandler(IUserRepository userRepo, IRepository<Customer> customerRepo)
        {
            _userRepo = userRepo;
            _customerRepo = customerRepo;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByUserNameAsync(request.UserName);
            if (user is null)
                throw new ApplicationException("User not found!");
            var customer = await _customerRepo.GetByIdAsync(request.Request.CustomerId);
            if(customer is null)
                throw new ApplicationException("Customer not found!");
            if(customer.UserId != user.Id)
                throw new ApplicationException("User and Customer not match!");

            user.FullName = request.Request.FullName ?? user.FullName;
            user.AvatarUrl = request.Request.AvatarUrl ?? user.AvatarUrl;
            user.PhoneNumber = request.Request.PhoneNumber ?? user.PhoneNumber;
            user.DoB = request.Request.DoB ?? user.DoB;
            user.Gender = request.Request.Gender ?? user.Gender;
            customer.PhoneNumber = request.Request.PhoneNumber ?? user.PhoneNumber;
            customer.Name = request.Request.FullName ?? user.FullName;
            customer.Address = request.Request.Address ?? customer.Address;

            _customerRepo.Update(customer);
            await _userRepo.UpdateUserAsync(user);
            await _customerRepo.SaveChangesAsync(cancellationToken);
            return "User is updated successfully";

        }
    }
}
