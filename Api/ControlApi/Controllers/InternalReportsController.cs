// ControlApi/Controllers/InternalReportsController.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTO.InternalReports;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ControlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternalReportsController : ControllerBase
    {
        private readonly IInternalReportService _service;

        public InternalReportsController(IInternalReportService service)
        {
            _service = service;
        }

        // GET: api/internalreports?status=&priority=&category=&professionalId=&teamId=&search=
        [HttpGet]
        public async Task<ActionResult<List<InternalReport>>> Get([FromQuery] InternalReportFiltersDto filters)
        {
            var reports = await _service.GetAsync(filters);
            return Ok(reports);
        }

        // GET: api/internalreports/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InternalReport>> GetById(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        // POST: api/internalreports
        [HttpPost]
        public async Task<ActionResult<InternalReport>> Create(CreateInternalReportDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/internalreports/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<InternalReport>> Update(int id, UpdateInternalReportDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/internalreports/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // POST: api/internalreports/{id}/comments
        [HttpPost("{id:int}/comments")]
        public async Task<ActionResult<InternalReportComment>> AddComment(int id, CreateInternalReportCommentDto dto)
        {
            var comment = await _service.AddCommentAsync(id, dto);
            return Ok(comment);
        }

        // GET: api/internalreports/professional/{professionalId}
        [HttpGet("professional/{professionalId:int}")]
        public async Task<ActionResult<List<InternalReport>>> ByProfessional(int professionalId)
        {
            var list = await _service.GetByProfessionalAsync(professionalId);
            return Ok(list);
        }

        // GET: api/internalreports/team/{teamId}
        [HttpGet("team/{teamId:int}")]
        public async Task<ActionResult<List<InternalReport>>> ByTeam(int teamId)
        {
            var list = await _service.GetByTeamAsync(teamId);
            return Ok(list);
        }

        // GET: api/internalreports/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<InternalReport>>> ByCategory(string category)
        {
            var list = await _service.GetByCategoryAsync(category);
            return Ok(list);
        }

        // GET: api/internalreports/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<InternalReport>>> ByStatus(string status)
        {
            var list = await _service.GetByStatusAsync(status);
            return Ok(list);
        }

        // GET: api/internalreports/priority/{priority}
        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<List<InternalReport>>> ByPriority(string priority)
        {
            var list = await _service.GetByPriorityAsync(priority);
            return Ok(list);
        }
    }
}
