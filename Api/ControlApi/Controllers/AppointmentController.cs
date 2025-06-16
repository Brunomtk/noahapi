using Core.DTO.Appointment;
using Core.Enums;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ControlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] AppointmentStatus? status = null,
            [FromQuery] string? search = null)
        {
            var resultPaged = await _appointmentService.GetPagedAppointments(page, pageSize, status, search);
            return Ok(resultPaged);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _appointmentService.GetById(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                Title = dto.Title,
                Address = dto.Address,
                Start = dto.Start,
                End = dto.End,
                CompanyId = dto.CompanyId,
                CustomerId = dto.CustomerId,
                TeamId = dto.TeamId,
                ProfessionalId = dto.ProfessionalId,
                Status = dto.Status ?? AppointmentStatus.Scheduled,
                Type = dto.Type ?? AppointmentType.Regular,
                Notes = dto.Notes
            };

            var success = await _appointmentService.Create(appointment);
            if (!success) return BadRequest("Erro ao criar agendamento.");

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDTO dto)
        {
            var existing = await _appointmentService.GetById(id);
            if (existing == null) return NotFound();

            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.Start.HasValue) existing.Start = dto.Start.Value;
            if (dto.End.HasValue) existing.End = dto.End.Value;
            if (dto.CompanyId.HasValue) existing.CompanyId = dto.CompanyId.Value;
            if (dto.CustomerId.HasValue) existing.CustomerId = dto.CustomerId;
            if (dto.TeamId.HasValue) existing.TeamId = dto.TeamId;
            if (dto.ProfessionalId.HasValue) existing.ProfessionalId = dto.ProfessionalId;
            if (dto.Status.HasValue) existing.Status = dto.Status.Value;
            if (dto.Type.HasValue) existing.Type = dto.Type.Value;
            if (dto.Notes != null) existing.Notes = dto.Notes;

            var success = await _appointmentService.Update(existing);
            if (!success) return BadRequest("Erro ao atualizar agendamento.");

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _appointmentService.GetById(id);
            if (existing == null) return NotFound();

            var success = await _appointmentService.Delete(existing);
            if (!success) return BadRequest("Erro ao excluir agendamento.");

            return NoContent();
        }
    }
}
