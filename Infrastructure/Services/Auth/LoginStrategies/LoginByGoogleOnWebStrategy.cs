﻿using Application.Services.Auth;
using Domain.Entities;
using Shared.Auth;

namespace Infrastructure.Services.Auth.LoginStrategies
{
    public class LoginByGoogleOnWebStrategy : ILoginStragy
    {
        public Task<IUser> Execute(LoginReq request)
        {
            throw new NotImplementedException();
        }
    }
}
