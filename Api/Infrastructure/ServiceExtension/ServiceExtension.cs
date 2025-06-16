using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        /// <summary>
        /// Configura os serviços de injeção de dependência (DI).
        /// </summary>
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Conexão com banco de dados PostgreSQL
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DbContextClass>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Repositórios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanSubscriptionRepository, PlanSubscriptionRepository>();
            services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>(); 

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
