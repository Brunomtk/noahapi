using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DbContextClass>(options =>
                options.UseNpgsql(connectionString)
            );

            // Repository registrations
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanSubscriptionRepository, PlanSubscriptionRepository>();
            services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICheckRecordRepository, CheckRecordRepository>();
            services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();
            services.AddScoped<IGpsTrackingRepository, GpsTrackingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IInternalFeedbackRepository, InternalFeedbackRepository>(); 

            // Service registrations
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
