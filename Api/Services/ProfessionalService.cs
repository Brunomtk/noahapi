using Core.DTO.Professional;
using Core.Enums;
using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // ? Necess�rio para usar ToList()
using Infrastructure.Repositories;
using Core.Models;

namespace Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfessionalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Professional>> GetAllProfessionals()
        {
            return (await _unitOfWork.Professionals.GetAll()).ToList(); // ? Corrigido
        }

        public async Task<Professional?> GetProfessionalById(int id)
        {
            return await _unitOfWork.Professionals.GetById(id);
        }

        public async Task<Professional> CreateProfessional(CreateProfessionalRequest request)
        {
            var professional = new Professional
            {
                Name = request.Name,
                Cpf = request.Cpf,
                Email = request.Email,
                Phone = request.Phone,
                Status = StatusEnum.Active,
                TeamId = request.TeamId,
                CompanyId = request.CompanyId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _unitOfWork.Professionals.Add(professional);
            _unitOfWork.Save();

            return professional;
        }

        public async Task<Professional?> UpdateProfessional(int id, UpdateProfessionalRequest request)
        {
            var professional = await _unitOfWork.Professionals.GetById(id);
            if (professional == null) return null;

            professional.Name = request.Name ?? professional.Name;
            professional.Cpf = request.Cpf ?? professional.Cpf;
            professional.Email = request.Email ?? professional.Email;
            professional.Phone = request.Phone ?? professional.Phone;
            professional.TeamId = request.TeamId ?? professional.TeamId;
            professional.Status = request.Status ?? professional.Status;
            professional.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.Professionals.Update(professional);
            _unitOfWork.Save();

            return professional;
        }

        public async Task<bool> DeleteProfessional(int id)
        {
            var professional = await _unitOfWork.Professionals.GetById(id);
            if (professional == null) return false;

            _unitOfWork.Professionals.Delete(professional);
            _unitOfWork.Save();

            return true;
        }
    }

    public interface IProfessionalService
    {
        Task<List<Professional>> GetAllProfessionals();
        Task<Professional?> GetProfessionalById(int id);
        Task<Professional> CreateProfessional(CreateProfessionalRequest request);
        Task<Professional?> UpdateProfessional(int id, UpdateProfessionalRequest request);
        Task<bool> DeleteProfessional(int id);
    }
}
