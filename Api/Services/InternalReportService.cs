// Services/InternalReportService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTO.InternalReports;
using Core.Enums;
using Core.Models;
using Infrastructure.Repositories;

namespace Services
{
    public class InternalReportService : IInternalReportService
    {
        private readonly IUnitOfWork _uow;
        public InternalReportService(IUnitOfWork uow) => _uow = uow;

        public Task<List<InternalReport>> GetAsync(InternalReportFiltersDto filters)
            => _uow.InternalReports.GetAsync(filters);

        public Task<InternalReport?> GetByIdAsync(int id)
            => _uow.InternalReports.GetByIdAsync(id);

        public async Task<InternalReport> CreateAsync(CreateInternalReportDto dto)
        {
            var report = new InternalReport
            {
                Title = dto.Title,
                ProfessionalId = dto.ProfessionalId,
                Professional = dto.ProfessionalId.ToString(),
                TeamId = dto.TeamId,
                Team = dto.TeamId.ToString(),
                Category = dto.Category,
                Status = !string.IsNullOrWhiteSpace(dto.Status)
                                    ? Enum.Parse<InternalReportStatus>(dto.Status, true)
                                    : InternalReportStatus.Pending,
                Date = dto.Date ?? DateTime.UtcNow,
                Description = dto.Description,
                Priority = !string.IsNullOrWhiteSpace(dto.Priority)
                                    ? Enum.Parse<InternalReportPriority>(dto.Priority, true)
                                    : InternalReportPriority.Medium,
                AssignedToId = dto.AssignedToId,
                AssignedTo = dto.AssignedToId.ToString(),
                Comments = new List<InternalReportComment>(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _uow.InternalReports.Add(report);
            await _uow.SaveAsync();
            return report;
        }

        public async Task<InternalReport?> UpdateAsync(int id, UpdateInternalReportDto dto)
        {
            var report = await _uow.InternalReports.GetByIdAsync(id);
            if (report == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Title))
                report.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.Description))
                report.Description = dto.Description;
            if (!string.IsNullOrWhiteSpace(dto.Category))
                report.Category = dto.Category;
            if (dto.Date.HasValue)
                report.Date = dto.Date.Value;
            if (!string.IsNullOrWhiteSpace(dto.Status))
                report.Status = Enum.Parse<InternalReportStatus>(dto.Status, true);
            if (!string.IsNullOrWhiteSpace(dto.Priority))
                report.Priority = Enum.Parse<InternalReportPriority>(dto.Priority, true);
            if (dto.ProfessionalId.HasValue)
            {
                report.ProfessionalId = dto.ProfessionalId.Value;
                report.Professional = dto.ProfessionalId.Value.ToString();
            }
            if (dto.TeamId.HasValue)
            {
                report.TeamId = dto.TeamId.Value;
                report.Team = dto.TeamId.Value.ToString();
            }
            if (dto.AssignedToId.HasValue)
            {
                report.AssignedToId = dto.AssignedToId.Value;
                report.AssignedTo = dto.AssignedToId.Value.ToString();
            }

            report.UpdatedDate = DateTime.UtcNow;
            _uow.InternalReports.Update(report);
            await _uow.SaveAsync();
            return report;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _uow.InternalReports.GetByIdAsync(id);
            if (report == null) return false;
            _uow.InternalReports.Delete(report);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<InternalReportComment> AddCommentAsync(int reportId, CreateInternalReportCommentDto dto)
        {
            var report = await _uow.InternalReports.GetByIdAsync(reportId);
            if (report == null)
                throw new KeyNotFoundException($"Report {reportId} not found");

            var comment = new InternalReportComment
            {
                InternalReportId = reportId,
                AuthorId = dto.AuthorId,
                Author = dto.AuthorId.ToString(),
                Text = dto.Text,
                Date = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            report.Comments.Add(comment);
            report.UpdatedDate = DateTime.UtcNow;
            _uow.InternalReports.Update(report);
            await _uow.SaveAsync();
            return comment;
        }

        public Task<List<InternalReport>> GetByProfessionalAsync(int professionalId)
            => _uow.InternalReports.GetByProfessionalAsync(professionalId);

        public Task<List<InternalReport>> GetByTeamAsync(int teamId)
            => _uow.InternalReports.GetByTeamAsync(teamId);

        public Task<List<InternalReport>> GetByCategoryAsync(string category)
            => _uow.InternalReports.GetByCategoryAsync(category);

        public Task<List<InternalReport>> GetByStatusAsync(string status)
            => _uow.InternalReports.GetByStatusAsync(status);

        public Task<List<InternalReport>> GetByPriorityAsync(string priority)
            => _uow.InternalReports.GetByPriorityAsync(priority);
    }

    public interface IInternalReportService
    {
        Task<List<InternalReport>> GetAsync(InternalReportFiltersDto filters);
        Task<InternalReport?> GetByIdAsync(int id);
        Task<InternalReport> CreateAsync(CreateInternalReportDto dto);
        Task<InternalReport?> UpdateAsync(int id, UpdateInternalReportDto dto);
        Task<bool> DeleteAsync(int id);
        Task<InternalReportComment> AddCommentAsync(int reportId, CreateInternalReportCommentDto dto);
        Task<List<InternalReport>> GetByProfessionalAsync(int professionalId);
        Task<List<InternalReport>> GetByTeamAsync(int teamId);
        Task<List<InternalReport>> GetByCategoryAsync(string category);
        Task<List<InternalReport>> GetByStatusAsync(string status);
        Task<List<InternalReport>> GetByPriorityAsync(string priority);
    }
}
