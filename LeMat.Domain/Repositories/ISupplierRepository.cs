using LeMat.Domain.Entities;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Repositories;

public interface ISupplierRepository {
	Task CreateAsync(Supplier supplier);

	Task<bool> DocumentExistsAsync(Document document);
	Task<List<Supplier>> GetAllAsync();
}