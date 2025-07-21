// ControlApi/Controllers/MaterialsController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.DTO.Materials;
using Infrastructure.ServiceExtension;
using Services;
using Core.Models;

namespace ControlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _service;

        public MaterialsController(IMaterialService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paged list of materials matching the specified filters.
        /// </summary>
        /// <param name="filters">Filtering and paging parameters</param>
        [HttpGet]
        public async Task<ActionResult<PagedResult<Material>>> Get([FromQuery] MaterialFiltersDto filters)
        {
            var pageResult = await _service.GetPagedAsync(filters);
            return Ok(pageResult);
        }

        /// <summary>
        /// Retrieves a single material by its ID.
        /// </summary>
        /// <param name="id">Material ID</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetById(int id)
        {
            var material = await _service.GetByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        /// <summary>
        /// Creates a new material.
        /// </summary>
        /// <param name="dto">Data for the new material</param>
        [HttpPost]
        public async Task<ActionResult<Material>> Create([FromBody] CreateMaterialDto dto)
        {
            var material = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = material.Id }, material);
        }

        /// <summary>
        /// Updates an existing material.
        /// </summary>
        /// <param name="id">Material ID</param>
        /// <param name="dto">Updated data</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<Material>> Update(int id, [FromBody] UpdateMaterialDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Deletes a material by its ID.
        /// </summary>
        /// <param name="id">Material ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
