using CleanArchiteture.Persistence.Context;
using CleanArchiteture.Persistence;
using CleanArchiteture.Application.Services;
using CleanArquiteture.WebAPI.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchiteture.Domain.Exceptions;
using CleanArquiteture.WebAPI.Middleware;

namespace CleanArquiteture.WebAPI
{
    public class Program
    {
        public static string _jwtSecret = "rbs38-8343fhye-64193-ndr27utrangplecy";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureBuilder(builder);

            var app = builder.Build();
            CreateDatabase(app);
            ConfigureApp(app);

            app.Run();
        }

        static void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.ConfigurePersistenceApp(builder.Configuration);
            builder.Services.ConfigureApplicationApp();
            builder.Services.ConfigureCorsPolicy();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMvc(options => 
            {
                options.Filters.Add(new ApiExceptionFilter());
            });


            ConfigSwagger(builder);

            ConfigureJWT(builder);
        }

        static void ConfigureApp(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseJwtCookieMiddleware();

            app.MapControllers();

            //======================= Configura��es de Headers =======================//
            ConfigureHeaders(app);
        }

        static void ConfigureHeaders(WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
                context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                if (app.Environment.IsProduction())
                {
                    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                }
                var allowedOrigins = new[] { "https://localhost:3000" };
                var origin = context.Request.Headers["Origin"].ToString();

                if (allowedOrigins.Contains(origin))
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
                }

                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

                await next();
            });
        }

        static void CreateDatabase(WebApplication app)
        {
            var serviceScope = app.Services.CreateScope();
            var dataContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

            //Comentado pois não estou disponibilizando um banco de dados!
            //dataContext?.Database.EnsureCreated();
        }

        static void ConfigSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.GetFullPath("CleanArchiteture.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CleanArchitetureAPI",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });
        }


        static void ConfigureJWT(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //Defino minha cháve de criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }


    }
}
