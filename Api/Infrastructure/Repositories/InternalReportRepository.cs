using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO.InternalReports;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InternalReportRepository : IInternalReportRepository
    {
        private readonly DbContextClass _dbContext;

        public InternalReportRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InternalReport>> GetAsync(InternalReportFiltersDto filters)
        {
            var query = _dbContext.InternalReports
                .Include(r => r.Comments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filters.Status) && filters.Status != "all")
                query = query.Where(r => r.Status.ToString() == filters.Status);

            if (!string.IsNullOrWhiteSpace(filters.Priority) && filters.Priority != "all")
                query = query.Where(r => r.Priority.ToString() == filters.Priority);

            if (!string.IsNullOrWhiteSpace(filters.Category))
                query = query.Where(r => r.Category == filters.Category);

            if (filters.ProfessionalId.HasValue)
                query = query.Where(r => r.ProfessionalId == filters.ProfessionalId.Value);

            if (filters.TeamId.HasValue)
                query = query.Where(r => r.TeamId == filters.TeamId.Value);

            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                var s = filters.Search.ToLower();
                query = query.Where(r =>
                    r.Title.ToLower().Contains(s) ||
                    r.Description.ToLower().Contains(s));
            }

            return await query
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();
        }

        public async Task<InternalReport?> GetByIdAsync(int id)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.Id == id);

        public void Add(InternalReport entity)
            => _dbContext.InternalReports.Add(entity);

        public void Update(InternalReport entity)
            => _dbContext.InternalReports.Update(entity);

        public void Delete(InternalReport entity)
            => _dbContext.InternalReports.Remove(entity);

        public async Task<List<InternalReport>> GetByProfessionalAsync(int professionalId)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .Where(r => r.ProfessionalId == professionalId)
                .ToListAsync();

        public async Task<List<InternalReport>> GetByTeamAsync(int teamId)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .Where(r => r.TeamId == teamId)
                .ToListAsync();

        public async Task<List<InternalReport>> GetByCategoryAsync(string category)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .Where(r => r.Category == category)
                .ToListAsync();

        public async Task<List<InternalReport>> GetByStatusAsync(string status)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .Where(r => r.Status.ToString() == status)
                .ToListAsync();

        public async Task<List<InternalReport>> GetByPriorityAsync(string priority)
            => await _dbContext.InternalReports
                .Include(r => r.Comments)
                .Where(r => r.Priority.ToString() == priority)
                .ToListAsync();
    }

    public interface IInternalReportRepository
    {
        Task<List<InternalReport>> GetAsync(InternalReportFiltersDto filters);
        Task<InternalReport?> GetByIdAsync(int id);
        void Add(InternalReport entity);
        void Update(InternalReport entity);
        void Delete(InternalReport entity);
        Task<List<InternalReport>> GetByProfessionalAsync(int professionalId);
        Task<List<InternalReport>> GetByTeamAsync(int teamId);
        Task<List<InternalReport>> GetByCategoryAsync(string category);
        Task<List<InternalReport>> GetByStatusAsync(string status);
        Task<List<InternalReport>> GetByPriorityAsync(string priority);
    }
}
