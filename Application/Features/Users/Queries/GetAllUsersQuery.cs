using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Users;

namespace Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserInfoRes>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserInfoRes>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<Customer> _customerRepo;

        public GetAllUsersQueryHandler(IUserRepository userRepo, IRepository<Customer> customerRepo)
        {
            _userRepo = userRepo;
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<UserInfoRes>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepo.GetAllAsync(cancellationToken);

            var userIds = users.Select(u => u.Id).ToList();
            var customers = await _customerRepo.GetAllAsync(c => userIds.Contains(c.UserId));

            var result = users.Select(user =>
            {
                var customer = customers.FirstOrDefault(c => c.UserId == user.Id);

                return new UserInfoRes
                {
                    FullName = user.FullName ?? customer?.Name,
                    PhoneNumber = user.PhoneNumber ?? customer?.PhoneNumber,
                    Email = user.Email,
                    Address = customer?.Address,
                    Gender = user.Gender.ToString(),
                    DoB = user.DoB,
                    AvatarUrl = user.AvatarUrl
                };
            });

            return result;
        }
    }
}
