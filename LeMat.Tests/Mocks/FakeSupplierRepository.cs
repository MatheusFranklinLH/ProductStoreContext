using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.ValueObjects;

namespace LeMat.Tests.Mocks;

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