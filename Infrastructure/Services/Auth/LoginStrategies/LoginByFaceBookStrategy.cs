using Application.Features.Auth;
using Application.Services.Auth;
using Domain.Entities;
using Shared.Auth;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public class LoginByFaceBookStrategy : ILoginStragy
    {
        public Task<IUser> Execute(LoginCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
