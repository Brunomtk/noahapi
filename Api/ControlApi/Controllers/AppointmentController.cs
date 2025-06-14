using Core.DTO.Appointment;
using Core.Enums;
using Core.Models;
using Infrastructure.ServiceExtension;
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

        /// <summary>
        /// Lista agendamentos com paginação, filtro por status e busca por título.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? status = null,
            [FromQuery] string? search = null)
        {
            var result = await _appointmentService.GetPagedAppointmentsAsync(page, pageSize, status, search);
            return Ok(result);
        }

        /// <summary>
        /// Busca um agendamento por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        /// <summary>
        /// Cria um novo agendamento.
        /// </summary>
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
                Status = Enum.Parse<AppointmentStatus>(dto.Status, true),
                Type = Enum.Parse<AppointmentType>(dto.Type, true),
                Notes = dto.Notes
            };

            var created = await _appointmentService.CreateAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um agendamento existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentDTO dto)
        {
            var existing = await _appointmentService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.Start.HasValue) existing.Start = dto.Start.Value;
            if (dto.End.HasValue) existing.End = dto.End.Value;
            if (dto.CompanyId.HasValue) existing.CompanyId = dto.CompanyId.Value;
            if (dto.CustomerId.HasValue) existing.CustomerId = dto.CustomerId;
            if (dto.TeamId.HasValue) existing.TeamId = dto.TeamId;
            if (dto.ProfessionalId.HasValue) existing.ProfessionalId = dto.ProfessionalId;
            if (dto.Status != null) existing.Status = Enum.Parse<AppointmentStatus>(dto.Status, true);
            if (dto.Type != null) existing.Type = Enum.Parse<AppointmentType>(dto.Type, true);
            if (dto.Notes != null) existing.Notes = dto.Notes;

            var updated = await _appointmentService.UpdateAsync(id, existing);
            return Ok(updated);
        }

        /// <summary>
        /// Remove um agendamento.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _appointmentService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
