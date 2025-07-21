using Core.DTO;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ServiceExtension;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContextClass dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Retorna uma empresa pelo CNPJ
        /// </summary>
        public async Task<Company?> GetByCnpj(string cnpj)
        {
            return await _dbContext.Set<Company>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }

        /// <summary>
        /// Retorna empresas com filtro por nome ou CNPJ (opcional)
        /// </summary>
        public async Task<PagedResult<Company>> GetAllCompaniesPaged(FiltersDTO filtersDTO)
        {
            return await _dbContext.Set<Company>()
                .AsNoTracking()
                .Include(c => c.Plan)
                .Where(x =>
                    (string.IsNullOrEmpty(filtersDTO.Name) || EF.Functions.Like(x.Name.ToLower(), $"%{filtersDTO.Name.ToLower()}%")) ||
                    (x.Cnpj != null && x.Cnpj.Contains(filtersDTO.Name))
                )
                .GetPagedAsync(filtersDTO.pageNumber, filtersDTO.pageSize);
        }
    }

    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetByCnpj(string cnpj);
        Task<PagedResult<Company>> GetAllCompaniesPaged(FiltersDTO filtersDTO);
    }
}
