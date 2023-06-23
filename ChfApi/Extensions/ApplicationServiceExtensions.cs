using ChfApi.Data;
using ChfApi.Interfaces;
using ChfApi.Repositories;
using Microsoft.EntityFrameworkCore;
using ChfApi.Services;
using Microsoft.AspNetCore.Identity;
using ChfApi.Entities;
using ChfApi.Helpers;

namespace ChfApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();

            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IReportServices, ReportServices>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            return services;
        }
    }
}
