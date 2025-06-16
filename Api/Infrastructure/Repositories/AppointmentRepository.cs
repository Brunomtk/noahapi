using Core.Enums;
using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.ServiceExtension;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DbContextClass context) : base(context) { }

        public async Task<PagedResult<Appointment>> GetPagedAppointmentsAsync(int page, int pageSize, AppointmentStatus? status = null, string? search = null)
        {
            var query = _dbContext.Set<Appointment>()
                .Include(a => a.Company)
                .Include(a => a.Customer)
                .Include(a => a.Team)
                .Include(a => a.Professional)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(a => a.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(a => a.Title.ToLowerInvariant().Contains(search.ToLowerInvariant()));

            return await query
                .OrderByDescending(a => a.Start)
                .GetPagedAsync<Appointment>(page, pageSize);
        }

        public async Task<List<Appointment>> GetAppointmentsByCompanyAsync(Guid companyId)
        {
            return await _dbContext.Set<Appointment>()
                .Include(a => a.Customer)
                .Include(a => a.Team)
                .Include(a => a.Professional)
                .Where(a => a.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByTeamAsync(Guid teamId)
        {
            return await _dbContext.Set<Appointment>()
                .Include(a => a.Company)
                .Include(a => a.Customer)
                .Include(a => a.Professional)
                .Where(a => a.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByProfessionalAsync(Guid professionalId)
        {
            return await _dbContext.Set<Appointment>()
                .Include(a => a.Company)
                .Include(a => a.Customer)
                .Include(a => a.Team)
                .Where(a => a.ProfessionalId == professionalId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByCustomerAsync(Guid customerId)
        {
            return await _dbContext.Set<Appointment>()
                .Include(a => a.Company)
                .Include(a => a.Team)
                .Include(a => a.Professional)
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end, Guid? companyId = null)
        {
            var query = _dbContext.Set<Appointment>()
                .Include(a => a.Company)
                .Include(a => a.Customer)
                .Include(a => a.Team)
                .Include(a => a.Professional)
                .Where(a => a.Start >= start && a.End <= end);

            if (companyId.HasValue)
                query = query.Where(a => a.CompanyId == companyId.Value);

            return await query.ToListAsync();
        }
    }
}
public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<PagedResult<Appointment>> GetPagedAppointmentsAsync(int page, int pageSize, AppointmentStatus? status = null, string? search = null);
    Task<List<Appointment>> GetAppointmentsByCompanyAsync(Guid companyId);
    Task<List<Appointment>> GetAppointmentsByTeamAsync(Guid teamId);
    Task<List<Appointment>> GetAppointmentsByProfessionalAsync(Guid professionalId);
    Task<List<Appointment>> GetAppointmentsByCustomerAsync(Guid customerId);
    Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end, Guid? companyId = null);
}