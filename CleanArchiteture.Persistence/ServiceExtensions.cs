using CleanArchiteture.Domain.Interfaces;
using CleanArchiteture.Persistence.Context;
using CleanArchiteture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
