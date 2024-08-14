﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArquiteture.WebAPI.AuthenticationServices;

namespace CleanArchiteture.Application.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplicationApp(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
