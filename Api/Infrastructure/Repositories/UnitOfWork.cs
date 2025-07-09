using System;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Core.Models;

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
        public ILeaderRepository Leaders { get; }
        public IAppointmentRepository Appointments { get; }
        public ICustomerRepository Customers { get; }
        public ICheckRecordRepository CheckRecords { get; }
        public IRecurrenceRepository Recurrences { get; }
        public IGpsTrackingRepository GpsTrackings { get; }
        public IReviewRepository Reviews { get; }
        public IInternalFeedbackRepository InternalFeedbacks { get; }

        public UnitOfWork(
            DbContextClass dbContext,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IPlanRepository planRepository,
            IPlanSubscriptionRepository planSubscriptionRepository,
            IProfessionalRepository professionalRepository,
            ITeamRepository teamRepository,
            ILeaderRepository leaderRepository,
            IAppointmentRepository appointmentRepository,
            ICustomerRepository customerRepository,
            ICheckRecordRepository checkRecordRepository,
            IRecurrenceRepository recurrenceRepository,
            IGpsTrackingRepository gpsTrackingRepository,
            IReviewRepository reviewRepository,
            IInternalFeedbackRepository internalFeedbackRepository
        )
        {
            _dbContext = dbContext;
            Users = userRepository;
            Companies = companyRepository;
            Plans = planRepository;
            PlanSubscriptions = planSubscriptionRepository;
            Professionals = professionalRepository;
            Teams = teamRepository;
            Leaders = leaderRepository;
            Appointments = appointmentRepository;
            Customers = customerRepository;
            CheckRecords = checkRecordRepository;
            Recurrences = recurrenceRepository;
            GpsTrackings = gpsTrackingRepository;
            Reviews = reviewRepository;
            InternalFeedbacks = internalFeedbackRepository;
        }

        public int Save()
            => _dbContext.SaveChanges();

        public Task<int> SaveAsync()
            => _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _dbContext.Dispose();
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
        ILeaderRepository Leaders { get; }
        IAppointmentRepository Appointments { get; }
        ICustomerRepository Customers { get; }
        ICheckRecordRepository CheckRecords { get; }
        IRecurrenceRepository Recurrences { get; }
        IGpsTrackingRepository GpsTrackings { get; }
        IReviewRepository Reviews { get; }
        IInternalFeedbackRepository InternalFeedbacks { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
