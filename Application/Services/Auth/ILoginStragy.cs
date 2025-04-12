using Domain.Entities;
using Shared.Auth;

namespace Application.Services.Auth
{
    public interface ILoginStragy
    {
        Task<IUser> Execute(LoginReq request);
    }
}
