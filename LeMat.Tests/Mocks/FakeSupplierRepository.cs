using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.ValueObjects;

namespace LeMat.Tests.Mocks;

public class FakeSupplierRepository : ISupplierRepository {

	public Task CreateAsync(Supplier supplier) {
		return Task.Delay(50);
	}

	public Task UpdateAsync(Supplier supplier) {
		return Task.Delay(50);
	}

	public Task DeleteAsync(Supplier supplier) {
		return Task.Delay(50);
	}

	public Task<bool> DocumentExistsAsync(Document document) {
		if (document.Type == EDocumentType.CPF && string.Equals(document.Number, "12312312312"))
			return Task.FromResult(true);
		return Task.FromResult(false);
	}

	public Task<List<Supplier>> GetAllAsync() {
		return Task.FromResult(new List<Supplier>());
	}

	public Task<Supplier> GetByIdAsync(int id) {
		Telephone telephone = new("(55) 99999-7620");
		Email email = new("123@gmail.com");
		Address address = new("1 street", "123", "Santa Cruz", "Gotham", "Massachussets", "EUA", "12313213");
		Document document = new("12312312312", EDocumentType.CPF);
		var supplier = new Supplier("fornecedor 1", "sadasd", telephone, email, address, document);
		return Task.FromResult(supplier);
	}

}