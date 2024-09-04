using LeMat.Domain.Entities;
using LeMat.Domain.Responses;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Repositories;

public interface ISupplierRepository {
	Task CreateAsync(Supplier supplier);
	Task UpdateAsync(Supplier supplier);
	Task DeleteAsync(Supplier supplier);
	Task<bool> DocumentExistsAsync(Document document);
	Task<List<SupplierResponse>> GetAllAsync();
	Task<Supplier> GetByIdAsync(int id);
}