// Services/MaterialService.cs
using System;
using System.Threading.Tasks;
using Core.DTO.Materials;
using Core.Enums;
using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.ServiceExtension;

namespace Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _uow;

        public MaterialService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<PagedResult<Material>> GetPagedAsync(MaterialFiltersDto filters)
            => _uow.Materials.GetPagedAsync(filters);

        public Task<Material?> GetByIdAsync(int id)
            => _uow.Materials.GetByIdAsync(id);

        public async Task<Material> CreateAsync(CreateMaterialDto dto)
        {
            var entity = new Material
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Unit = dto.Unit,
                CurrentStock = dto.CurrentStock,
                MinStock = dto.MinStock,
                MaxStock = dto.MaxStock,
                UnitPrice = dto.UnitPrice,
                Supplier = dto.Supplier,
                SupplierContact = dto.SupplierContact,
                Barcode = dto.Barcode,
                Location = dto.Location,
                ExpirationDate = dto.ExpirationDate,
                Status = Enum.Parse<MaterialStatus>(dto.Status, ignoreCase: true),
                CompanyId = dto.CompanyId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _uow.Materials.Add(entity);
            await _uow.SaveAsync();
            return entity;
        }

        public async Task<Material?> UpdateAsync(int id, UpdateMaterialDto dto)
        {
            var entity = await _uow.Materials.GetByIdAsync(id);
            if (entity == null) return null;

            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Description != null) entity.Description = dto.Description;
            if (dto.Category != null) entity.Category = dto.Category;
            if (dto.Unit != null) entity.Unit = dto.Unit;
            if (dto.CurrentStock.HasValue) entity.CurrentStock = dto.CurrentStock.Value;
            if (dto.MinStock.HasValue) entity.MinStock = dto.MinStock.Value;
            if (dto.MaxStock.HasValue) entity.MaxStock = dto.MaxStock;
            if (dto.UnitPrice.HasValue) entity.UnitPrice = dto.UnitPrice.Value;
            if (dto.Supplier != null) entity.Supplier = dto.Supplier;
            if (dto.SupplierContact != null) entity.SupplierContact = dto.SupplierContact;
            if (dto.Barcode != null) entity.Barcode = dto.Barcode;
            if (dto.Location != null) entity.Location = dto.Location;
            if (dto.ExpirationDate != null) entity.ExpirationDate = dto.ExpirationDate;
            if (dto.Status != null)
                entity.Status = Enum.Parse<MaterialStatus>(dto.Status, ignoreCase: true);
            // CompanyId não é atualizado

            entity.UpdatedDate = DateTime.UtcNow;
            _uow.Materials.Update(entity);
            await _uow.SaveAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Materials.GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Materials.Delete(entity);
            await _uow.SaveAsync();
            return true;
        }
    }

    public interface IMaterialService
    {
        Task<PagedResult<Material>> GetPagedAsync(MaterialFiltersDto filters);
        Task<Material?> GetByIdAsync(int id);
        Task<Material> CreateAsync(CreateMaterialDto dto);
        Task<Material?> UpdateAsync(int id, UpdateMaterialDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
