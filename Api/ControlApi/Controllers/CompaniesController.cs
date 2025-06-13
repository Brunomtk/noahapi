using Core.DTO;
using Core.DTO.Company;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
        {
            var company = new Company
            {
                Name = request.Name,
                Cnpj = request.Cnpj,
                Responsible = request.Responsible,
                Email = request.Email,
                Phone = request.Phone,
                PlanId = request.PlanId,
                Status = request.Status,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var created = await _companyService.CreateCompany(company);
            return created ? Ok(true) : BadRequest("Failed to create company");
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetById(int companyId)
        {
            var company = await _companyService.GetCompanyById(companyId);
            return company != null ? Ok(company) : NotFound("Company not found");
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetAllPaged([FromQuery] FiltersDTO filters)
        {
            var result = await _companyService.GetAllCompaniesPaged(filters);

            return Ok(new
            {
                data = result.Results,
                meta = new
                {
                    currentPage = result.CurrentPage,
                    totalPages = result.PageCount,
                    totalItems = result.TotalItems,
                    itemsPerPage = result.PageSize
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> Update(int companyId, [FromBody] CreateCompanyRequest request)
        {
            var updated = await _companyService.UpdateCompany(request, companyId);
            return updated ? Ok(true) : BadRequest("Failed to update company");
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> Delete(int companyId)
        {
            var deleted = await _companyService.DeleteCompany(companyId);
            return deleted ? Ok(true) : NotFound("Company not found");
        }
    }
}
