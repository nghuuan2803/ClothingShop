using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Users;

namespace Application.Features.Users.Queries
{
    public class GetUserInfoQuery : IRequest<UserInfoRes>
    {
        public string UserName { get; set; }
    }
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoRes>
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<Customer> _customerRepo;

        public GetUserInfoQueryHandler(IUserRepository userRepo, IRepository<Customer> customerRepo)
        {
            _userRepo = userRepo;
            _customerRepo = customerRepo;
        }

        public async Task<UserInfoRes> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            IUser user = await _userRepo.GetByUserNameAsync(request.UserName);
            if (user is null)
                throw new ApplicationException("User not found!");
            var customer = await _customerRepo.GetSingleAsync(p => p.UserId == user.Id);
            return new UserInfoRes
            {
                FullName = user.FullName ?? customer.Name,
                PhoneNumber = user.PhoneNumber ?? customer.PhoneNumber,
                Email = user.Email,
                Address = customer.Address,
                Gender = user.Gender.ToString(),
                DoB = user.DoB,
                AvatarUrl = user.AvatarUrl
            };
        }
    }
}
