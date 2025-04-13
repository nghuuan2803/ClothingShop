using Application.Services.Auth;
using MediatR;
using Shared.Auth;

namespace Application.Features.Auth
{
    public class RefreshTokensCommand : IRequest<AuthRes>
    {
        public string RefreshToken { get; set; }
    }
    public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, AuthRes>
    {
        private IAuthService _authService;

        public RefreshTokensCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthRes> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokensAsync(request.RefreshToken);
        }
    }
}
