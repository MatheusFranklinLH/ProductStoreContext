using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Domain.Repositories;
using StoreProductsContext.Domain.ValueObjects;

namespace StoreProductsContext.Tests.Mocks;

public class FakeSupplierRepository : ISupplierRepository {

	public void Create(Supplier supplier) { }

	public bool DocumentExists(Document document) {
		if (document.Type == EDocumentType.CPF && string.Equals(document.Number, "12312312312"))
			return true;
		return false;
	}

	public IEnumerable<Supplier> GetAll() {
		return new List<Supplier>();
	}

}