using Core;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class ProfessionalRepository : GenericRepository<Professional>, IProfessionalRepository
    {
        private readonly DbContextClass _dbContext;

        public ProfessionalRepository(DbContextClass dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Professional>> GetAllAsync()
        {
            return await _dbContext.Professionals
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Professional?> GetByIdAsync(int id)
        {
            return await _dbContext.Professionals
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
public interface IProfessionalRepository : IGenericRepository<Professional>
{
    Task<List<Professional>> GetAllAsync();
    Task<Professional?> GetByIdAsync(int id);
}