// Infrastructure/Repositories/ReviewRepository.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO.Review;
using Core.Enums;
using Infrastructure.ServiceExtension;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly DbContextClass _context;

        public ReviewRepository(DbContextClass context) : base(context)
            => _context = context;

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);      // agora r.Id (int) == id (int)
        }

        public async Task<PagedResult<Review>> GetPagedAsync(ReviewFiltersDTO filters)
        {
            var q = _context.Reviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filters.Status)
                && !filters.Status.Equals("all", StringComparison.OrdinalIgnoreCase)
                && Enum.TryParse<ReviewStatus>(filters.Status, true, out var statusEnum))
            {
                q = q.Where(x => x.Status == statusEnum);
            }

            if (!string.IsNullOrWhiteSpace(filters.Rating)
                && !filters.Rating.Equals("all", StringComparison.OrdinalIgnoreCase)
                && int.TryParse(filters.Rating, out var ratingValue))
            {
                q = q.Where(x => x.Rating == ratingValue);
            }

            if (!string.IsNullOrWhiteSpace(filters.SearchQuery))
            {
                var txt = filters.SearchQuery.ToLower();
                q = q.Where(x =>
                    (x.CustomerName ?? "").ToLower().Contains(txt) ||
                    (x.ProfessionalName ?? "").ToLower().Contains(txt) ||
                    (x.CompanyName ?? "").ToLower().Contains(txt) ||
                    (x.Comment ?? "").ToLower().Contains(txt)
                );
            }

            return await q
                .OrderByDescending(x => x.CreatedDate)
                .GetPagedAsync(filters.PageNumber, filters.PageSize);
        }
    }
}

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<Review?> GetByIdAsync(int id);                // id agora é int
    Task<PagedResult<Review>> GetPagedAsync(ReviewFiltersDTO filters);
}