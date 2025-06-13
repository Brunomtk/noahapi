using System;
using System.Threading.Tasks;
using Core.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;

        public IUserRepository Users { get; }
        public ICompanyRepository Companies { get; }
        public IPlanRepository Plans { get; }
        public IPlanSubscriptionRepository PlanSubscriptions { get; }
        public IProfessionalRepository Professionals { get; }
        public ITeamRepository Teams { get; }
        public ILeaderRepository Leaders { get; } // ? repositório de líderes

        public UnitOfWork(
            DbContextClass dbContext,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IPlanRepository planRepository,
            IPlanSubscriptionRepository planSubscriptionRepository,
            IProfessionalRepository professionalRepository,
            ITeamRepository teamRepository,
            ILeaderRepository leaderRepository)
        {
            _dbContext = dbContext;
            Users = userRepository;
            Companies = companyRepository;
            Plans = planRepository;
            PlanSubscriptions = planSubscriptionRepository;
            Professionals = professionalRepository;
            Teams = teamRepository;
            Leaders = leaderRepository; // ? injeção do repositório de Leader
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ICompanyRepository Companies { get; }
    IPlanRepository Plans { get; }
    IPlanSubscriptionRepository PlanSubscriptions { get; }
    IProfessionalRepository Professionals { get; }
    ITeamRepository Teams { get; }
    ILeaderRepository Leaders { get; } // ? necessário para LeaderService

    int Save();
    Task<int> SaveAsync(); // ? usado em serviços assíncronos
}