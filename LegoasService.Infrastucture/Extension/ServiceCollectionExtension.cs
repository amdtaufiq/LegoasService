using LegoasService.Core.Interfaces.Repositories;
using LegoasService.Core.Interfaces.Services;
using LegoasService.Core.Interfaces.Unit;
using LegoasService.Core.Services.HelperServices;
using LegoasService.Core.Services.MainServices;
using LegoasService.Infrastucture.Data;
using LegoasService.Infrastucture.Repositories;
using LegoasService.Infrastucture.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LegoasService.Infrastucture.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BEDBContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("connection")),
                ServiceLifetime.Transient);
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Repository
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Main Service
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IOfficerService, OfficerService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOfficeService, OfficeService>();

            //Helper Service
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<ITokenService, TokenService>();

            //Unit
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Service API",
                    Version = "v1"
                });

                doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using bearer scheme."
                });

                doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            return services;
        }
    }
}
