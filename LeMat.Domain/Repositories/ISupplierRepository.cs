using LeMat.Domain.Entities;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Repositories;

public interface ISupplierRepository {
	void Create(Supplier supplier);

	bool DocumentExists(Document document);
	IEnumerable<Supplier> GetAll();
}