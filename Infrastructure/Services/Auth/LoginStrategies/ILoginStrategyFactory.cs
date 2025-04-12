using Application.Services.Auth;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public interface ILoginStrategyFactory
    {
        ILoginStragy GetStragy();
    }
}
