using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.ServiceExtension;

namespace Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Appointment>> GetPagedAppointmentsAsync(int page, int pageSize, string? status = null, string? search = null)
        {
            return await _unitOfWork.Appointments.GetPagedAppointmentsAsync(page, pageSize, status, search);
        }

        public async Task<List<Appointment>> GetAppointmentsByCompanyAsync(int companyId)
        {
            return await _unitOfWork.Appointments.GetAppointmentsByCompanyAsync(companyId);
        }

        public async Task<List<Appointment>> GetAppointmentsByTeamAsync(int teamId)
        {
            return await _unitOfWork.Appointments.GetAppointmentsByTeamAsync(teamId);
        }

        public async Task<List<Appointment>> GetAppointmentsByProfessionalAsync(int professionalId)
        {
            return await _unitOfWork.Appointments.GetAppointmentsByProfessionalAsync(professionalId);
        }

        public async Task<List<Appointment>> GetAppointmentsByCustomerAsync(int customerId)
        {
            return await _unitOfWork.Appointments.GetAppointmentsByCustomerAsync(customerId);
        }

        public async Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end, int? companyId = null)
        {
            return await _unitOfWork.Appointments.GetAppointmentsByDateRangeAsync(start, end, companyId);
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Appointments.GetById(id);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            await _unitOfWork.Appointments.Add(appointment);
            await _unitOfWork.SaveAsync();
            return appointment;
        }

        public async Task<Appointment?> UpdateAsync(int id, Appointment updatedAppointment)
        {
            var existing = await _unitOfWork.Appointments.GetById(id);
            if (existing == null) return null;

            existing.Title = updatedAppointment.Title;
            existing.Address = updatedAppointment.Address;
            existing.Start = updatedAppointment.Start;
            existing.End = updatedAppointment.End;
            existing.Status = updatedAppointment.Status;
            existing.Type = updatedAppointment.Type;
            existing.Notes = updatedAppointment.Notes;
            existing.CompanyId = updatedAppointment.CompanyId;
            existing.CustomerId = updatedAppointment.CustomerId;
            existing.TeamId = updatedAppointment.TeamId;
            existing.ProfessionalId = updatedAppointment.ProfessionalId;
            existing.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Appointments.Update(existing);
            await _unitOfWork.SaveAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _unitOfWork.Appointments.GetById(id);
            if (existing == null) return false;

            _unitOfWork.Appointments.Delete(existing);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
public interface IAppointmentService
{
    Task<PagedResult<Appointment>> GetPagedAppointmentsAsync(int page, int pageSize, string? status = null, string? search = null);
    Task<List<Appointment>> GetAppointmentsByCompanyAsync(int companyId);
    Task<List<Appointment>> GetAppointmentsByTeamAsync(int teamId);
    Task<List<Appointment>> GetAppointmentsByProfessionalAsync(int professionalId);
    Task<List<Appointment>> GetAppointmentsByCustomerAsync(int customerId);
    Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end, int? companyId = null);

    Task<Appointment?> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> UpdateAsync(int id, Appointment updatedAppointment);
    Task<bool> DeleteAsync(int id);
}