using Core.DTO.Teams;
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
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Lista times com paginação, filtro por status e busca por nome.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string status = "all",
            [FromQuery] string? search = null)
        {
            var result = await _teamService.GetPagedTeams(page, pageSize, status, search);
            return Ok(result);
        }

        /// <summary>
        /// Retorna um time pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

        /// <summary>
        /// Cria um novo time.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamDTO dto)
        {
            var team = new Team
            {
                Name = dto.Name,
                LeaderId = dto.LeaderId,
                Region = dto.Region,
                Description = dto.Description,
                CompanyId = dto.CompanyId,
                Status = Core.Enums.StatusEnum.Active,
                Rating = 0,
                CompletedServices = 0,
            };

            var created = await _teamService.CreateAsync(team);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um time existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Team updatedTeam)
        {
            var result = await _teamService.UpdateAsync(id, updatedTeam);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Deleta um time.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teamService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
