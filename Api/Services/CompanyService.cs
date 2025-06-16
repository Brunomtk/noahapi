using Core.DTO;
using Core.DTO.Company;
using Core.Enums;
using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.ServiceExtension;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCompany(Company company)
        {
            if (company == null) return false;

            await _unitOfWork.Companies.Add(company);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _unitOfWork.Companies.GetAll();
        }

        public async Task<PagedResult<Company>> GetAllCompaniesPaged(FiltersDTO filtersDTO)
        {
            return await _unitOfWork.Companies.GetAllCompaniesPaged(filtersDTO);
        }

        public async Task<Company?> GetCompanyById(int companyId)
        {
            if (companyId <= 0) return null;
            return await _unitOfWork.Companies.GetById(companyId);
        }

        public async Task<Company?> GetCompanyByCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return null;
            return await _unitOfWork.Companies.GetByCnpj(cnpj);
        }

        public async Task<bool> UpdateCompany(CreateCompanyRequest request, int companyId)
        {
            var company = await _unitOfWork.Companies.GetById(companyId);
            if (company == null) return false;

            company.Name = request.Name;
            company.Cnpj = request.Cnpj;
            company.Responsible = request.Responsible;
            company.Email = request.Email;
            company.Phone = request.Phone;
            company.PlanId = request.PlanId;
            company.Status = request.Status;

            _unitOfWork.Companies.Update(company);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            var company = await _unitOfWork.Companies.GetById(companyId);
            if (company == null) return false;

            _unitOfWork.Companies.Delete(company);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }
    }

    public interface ICompanyService
    {
        Task<bool> CreateCompany(Company company);
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<PagedResult<Company>> GetAllCompaniesPaged(FiltersDTO filtersDTO);
        Task<Company?> GetCompanyById(int companyId);
        Task<Company?> GetCompanyByCnpj(string cnpj);
        Task<bool> UpdateCompany(CreateCompanyRequest request, int companyId);
        Task<bool> DeleteCompany(int companyId);
    }
}
