using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Shared.Auth;

namespace Application.Features.Auth
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public required string GuestId { get; set; }
        public required string LoginType { get; set; }  // "default", "google-web", "google-mobile"
        public required string Credential { get; set; } // "Email|Password","AuthCode" ,"IDToken"
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private IAuthService _authService;
        private IRepository<Customer> _customerRepo;
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authRes = await _authService.LoginAsync(request);
            var customer = await _customerRepo.GetSingleAsync(p => p.UserId == authRes.UserId);
            if(customer is null)
            {
                throw new ApplicationException("User không hợp lệ");
            }
            var response = new AuthResponse
            {
                AccessToken = authRes.AccessToken,
                RefreshToken = authRes.RefreshToken,
                Success = authRes.Success,
                CustomerId = customer.Id
            };
            return response;
        }
    }
}
