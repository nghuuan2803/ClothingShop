using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Auth;
using Shared.Users;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<AuthRes>
    {
        public AddUserReq Request { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthRes>
    {
        private IRepository<Customer> _customerRepo;
        private IUserRepository _userRepo;
        private IAuthService _authService;
        private ISender _sender;
        private IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IRepository<Customer> customerRepo, IUserRepository userRepo, IAuthService authService, ISender sender, IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _userRepo = userRepo;
            _authService = authService;
            _sender = sender;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthRes> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            //if (string.IsNullOrEmpty(command.Request.GuestId) || !Guid.TryParse(command.Request.GuestId, out _))
            //{
            //    throw new ArgumentException("Invalid GuestId");
            //}

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var guest = await _customerRepo.GetByIdAsync(command.Request.GuestId);
                if (guest == null)
                {
                    guest = new Customer { Id = command.Request.GuestId };
                    await _customerRepo.AddAsync(guest);
                }

                guest.IsRegistered = true;
                var user = await _userRepo.AddUserAsync(command.Request);
                guest.UserId = user.Id;
                guest.Name = command.Request.FullName;

                _customerRepo.Update(guest);
                await _unitOfWork.CommitTransactionAsync();

                var accessToken = await _authService.GenerateAccessTokenAsync(user);
                var refreshToken = _authService.GenerateRefreshToken();
                return new AuthRes
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Success = true
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
