using System;
using System.Threading.Tasks;
using Core.DTO.Review;
using Core.Models;
using Infrastructure.ServiceExtension;
using Infrastructure.Repositories;

namespace Services
{
    public interface IReviewService
    {
        Task<PagedResult<Review>> GetPagedAsync(ReviewFiltersDTO filters);
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(CreateReviewDTO dto);
        Task<Review?> UpdateAsync(int id, UpdateReviewDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<Review?> RespondAsync(int id, string response);
    }

    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public Task<PagedResult<Review>> GetPagedAsync(ReviewFiltersDTO filters)
            => _unitOfWork.Reviews.GetPagedAsync(filters);

        public Task<Review?> GetByIdAsync(int id)
            => _unitOfWork.Reviews.GetByIdAsync(id);

        public async Task<Review> CreateAsync(CreateReviewDTO dto)
        {
            var model = new Review
            {
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                ProfessionalId = dto.ProfessionalId,
                ProfessionalName = dto.ProfessionalName,
                TeamId = dto.TeamId,
                TeamName = dto.TeamName,
                CompanyId = dto.CompanyId,
                CompanyName = dto.CompanyName,
                AppointmentId = dto.AppointmentId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                Date = dto.Date,
                ServiceType = dto.ServiceType,
                Status = dto.Status,
                Response = dto.Response,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _unitOfWork.Reviews.Add(model);
            await _unitOfWork.SaveAsync();
            return model;
        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDTO dto)
        {
            var model = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (model == null) return null;

            if (!string.IsNullOrEmpty(dto.CustomerId)) model.CustomerId = dto.CustomerId;
            if (!string.IsNullOrEmpty(dto.CustomerName)) model.CustomerName = dto.CustomerName;
            if (!string.IsNullOrEmpty(dto.ProfessionalId)) model.ProfessionalId = dto.ProfessionalId;
            if (!string.IsNullOrEmpty(dto.ProfessionalName)) model.ProfessionalName = dto.ProfessionalName;
            if (!string.IsNullOrEmpty(dto.TeamId)) model.TeamId = dto.TeamId;
            if (!string.IsNullOrEmpty(dto.TeamName)) model.TeamName = dto.TeamName;
            if (!string.IsNullOrEmpty(dto.CompanyId)) model.CompanyId = dto.CompanyId;
            if (!string.IsNullOrEmpty(dto.CompanyName)) model.CompanyName = dto.CompanyName;
            if (!string.IsNullOrEmpty(dto.AppointmentId)) model.AppointmentId = dto.AppointmentId;
            if (dto.Rating.HasValue) model.Rating = dto.Rating.Value;
            if (!string.IsNullOrEmpty(dto.Comment)) model.Comment = dto.Comment;
            if (dto.Date.HasValue) model.Date = dto.Date.Value;
            if (!string.IsNullOrEmpty(dto.ServiceType)) model.ServiceType = dto.ServiceType;
            if (dto.Status.HasValue) model.Status = dto.Status.Value;
            if (!string.IsNullOrEmpty(dto.Response)) model.Response = dto.Response;
            if (dto.ResponseDate.HasValue) model.ResponseDate = dto.ResponseDate.Value;

            model.UpdatedDate = DateTime.UtcNow;
            _unitOfWork.Reviews.Update(model);
            await _unitOfWork.SaveAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (model == null) return false;

            _unitOfWork.Reviews.Delete(model);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Review?> RespondAsync(int id, string response)
        {
            var model = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (model == null) return null;

            model.Response = response;
            model.ResponseDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Reviews.Update(model);
            await _unitOfWork.SaveAsync();
            return model;
        }
    }
}
