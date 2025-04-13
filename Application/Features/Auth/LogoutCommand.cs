using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Auth
{
    public class LogoutCommand : IRequest<string>
    {
        public string? RefreshToken { get; set; }
    }
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, string>
    {
        private IAuthService _authService;

        public LogoutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<string> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LogoutAsync(request.RefreshToken);
        }
    }
}
