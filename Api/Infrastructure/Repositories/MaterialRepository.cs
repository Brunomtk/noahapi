// Infrastructure/Repositories/MaterialRepository.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO.Materials;
using Core.Enums.Material;
using Core.Models;
using Infrastructure.ServiceExtension;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly DbContextClass _dbContext;

        public MaterialRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Material>> GetPagedAsync(MaterialFiltersDto filters)
        {
            var query = _dbContext.Materials.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filters.Category))
                query = query.Where(m => m.Category == filters.Category);

            // Ajuste: converte filters.Status (string) para enum MaterialStatus
            if (!string.IsNullOrWhiteSpace(filters.Status) && !filters.Status.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                if (Enum.TryParse<MaterialStatus>(filters.Status, true, out var statusEnum))
                {
                    query = query.Where(m => m.Status == statusEnum);
                }
            }

            if (!string.IsNullOrWhiteSpace(filters.Supplier))
                query = query.Where(m => m.Supplier == filters.Supplier);

            if (filters.LowStock == true)
                query = query.Where(m => m.CurrentStock <= m.MinStock);

            if (filters.Expiring == true)
            {
                var today = DateTime.UtcNow.Date;
                var threshold = today.AddDays(30);
                query = query.Where(m =>
                    m.ExpirationDate.HasValue &&
                    m.ExpirationDate.Value.Date > today &&
                    m.ExpirationDate.Value.Date <= threshold);
            }

            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                var s = filters.Search.ToLower();
                query = query.Where(m =>
                    m.Name.ToLower().Contains(s) ||
                    (m.Description != null && m.Description.ToLower().Contains(s)) ||
                    m.Category.ToLower().Contains(s));
            }

            if (!string.IsNullOrWhiteSpace(filters.SortBy))
            {
                var desc = string.Equals(filters.SortOrder, "desc", StringComparison.OrdinalIgnoreCase);
                query = filters.SortBy switch
                {
                    "name" => desc ? query.OrderByDescending(m => m.Name) : query.OrderBy(m => m.Name),
                    "category" => desc ? query.OrderByDescending(m => m.Category) : query.OrderBy(m => m.Category),
                    "stock" => desc ? query.OrderByDescending(m => m.CurrentStock) : query.OrderBy(m => m.CurrentStock),
                    "price" => desc ? query.OrderByDescending(m => m.UnitPrice) : query.OrderBy(m => m.UnitPrice),
                    "updated" => desc ? query.OrderByDescending(m => m.UpdatedDate) : query.OrderBy(m => m.UpdatedDate),
                    _ => query
                };
            }

            return await query.GetPagedAsync(filters.Page, filters.PageSize);
        }

        public Task<Material?> GetByIdAsync(int id)
            => _dbContext.Materials.FindAsync(id).AsTask();

        public void Add(Material entity)
            => _dbContext.Materials.Add(entity);

        public void Update(Material entity)
            => _dbContext.Materials.Update(entity);

        public void Delete(Material entity)
            => _dbContext.Materials.Remove(entity);
    }

    public interface IMaterialRepository
    {
        Task<PagedResult<Material>> GetPagedAsync(MaterialFiltersDto filters);
        Task<Material?> GetByIdAsync(int id);
        void Add(Material entity);
        void Update(Material entity);
        void Delete(Material entity);
    }
}
