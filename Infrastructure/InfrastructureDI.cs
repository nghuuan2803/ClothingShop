using Application.Services.Auth;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrasutructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            })
               .AddIdentity<AppUser, IdentityRole>(options =>
               {
                   options.SignIn.RequireConfirmedAccount = false;
                   options.Password.RequireDigit = false;
                   options.Password.RequiredLength = 6;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireLowercase = false;
                   options.User.RequireUniqueEmail = false;
               }).AddEntityFrameworkStores<AppDbContext>().AddSignInManager();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // Chỉ định đường dẫn của hub
                        var path = context.HttpContext.Request.Path;
                        //if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<ProductVariant>, VariantRepository>();
            services.AddScoped<IRepository<CartItem>, CartRepository>();

            return services;
        }
    }
}
