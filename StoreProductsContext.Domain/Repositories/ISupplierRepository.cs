using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.ValueObjects;

namespace StoreProductsContext.Domain.Repositories;

public interface ISupplierRepository {
	void Create(Supplier supplier);

	bool DocumentExists(Document document);
	IEnumerable<Supplier> GetAll();
}