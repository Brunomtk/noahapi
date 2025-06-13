using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.ServiceExtension;
using Core.DTO;
using Core.Enums;

namespace Infrastructure.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(DbContextClass context) : base(context) { }

        public async Task<PagedResult<Team>> GetPagedTeams(int page, int pageSize, string status = "all", string? search = null)
        {
            var query = _dbContext.Set<Team>()
                .Include(t => t.Company)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
            {
                StatusEnum? statusEnum = status.ToLower() switch
                {
                    "ativo" => StatusEnum.Active,
                    "inativo" => StatusEnum.Inactive,
                    _ => null
                };

                if (statusEnum.HasValue)
                    query = query.Where(t => t.Status == statusEnum.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.ToLower().Contains(search.ToLower()));
            }

            return await query.OrderByDescending(t => t.CreatedDate).GetPagedAsync<Team>(page, pageSize);
        }
    }

    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<PagedResult<Team>> GetPagedTeams(int page, int pageSize, string status = "all", string? search = null);
    }
}
