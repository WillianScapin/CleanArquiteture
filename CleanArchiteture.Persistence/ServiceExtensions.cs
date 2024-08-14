using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArchiteture.Domain.Validators;
using CleanArchiteture.Persistence.Context;
using CleanArchiteture.Persistence.Repositories;
using CleanArquiteture.WebAPI.AuthenticationServices;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchiteture.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<IUnitOfWork, UnitiOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            

            services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();

            services.AddTransient<IValidator<User>, UserValidator>();
        }
    }
}
