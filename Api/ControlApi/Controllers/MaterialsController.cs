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
        /// Lista materiais com paginação e filtros.
        /// </summary>
        /// <param name="filters">Filtros da listagem (inclui CompanyId)</param>
        [HttpGet]
        public async Task<ActionResult<PagedResult<Material>>> Get([FromQuery] MaterialFiltersDto filters)
        {
            if (filters.CompanyId <= 0)
                return BadRequest("É necessário informar um CompanyId válido.");

            var pageResult = await _service.GetPagedAsync(filters);
            return Ok(pageResult);
        }

        /// <summary>
        /// Retorna um material pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetById(int id)
        {
            var material = await _service.GetByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        /// <summary>
        /// Cria um novo material.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Material>> Create([FromBody] CreateMaterialDto dto)
        {
            var material = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = material.Id }, material);
        }

        /// <summary>
        /// Atualiza um material existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Material>> Update(int id, [FromBody] UpdateMaterialDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Exclui um material.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
