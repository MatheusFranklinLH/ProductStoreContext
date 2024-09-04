using LeMat.Domain.Handlers;
using LeMat.Domain.Requests;
using LeMat.Tests.Mocks;

namespace LeMat.Tests;

[TestClass]
public class SupplierHandlerTests {
	[TestMethod]
	public async Task CreateShouldReturnErrorWhenDocumentExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository(), new FakeTransactionRepository());
		var request = new CreateSupplierRequest() {
			Name = "Colchões S/A",
			CompanyReason = "Colchões",
			Telephone = "38999999999",
			Document = "12312312312",
			DocumentIsCPF = true,
			Email = "colchoes@gmail.com",
			Street = "Rua x",
			Number = "198",
			Neighborhood = "Centro",
			City = "Gotham",
			State = "New York",
			Country = "Brasil",
			ZipCode = "12312312"
		};

		await handler.Handle(request);
		Assert.AreEqual(false, handler.IsValid);
	}

	[TestMethod]
	public async Task CreateShouldReturnSuccessWhenDocumentNotExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository(), new FakeTransactionRepository());
		var request = new CreateSupplierRequest() {
			Name = "Colchões S/A",
			CompanyReason = "Colchões",
			Telephone = "38999999999",
			Document = "11111111111",
			DocumentIsCPF = true,
			Email = "colchoes@gmail.com",
			Street = "Rua x",
			Number = "198",
			Neighborhood = "Centro",
			City = "Gotham",
			State = "New York",
			Country = "Brasil",
			ZipCode = "12312312"
		};

		await handler.Handle(request);
		Assert.AreEqual(true, handler.IsValid);
	}

	[TestMethod]
	public async Task UpdateShouldReturnErrorWhenDocumentExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository(), new FakeTransactionRepository());
		var request = new UpdateSupplierRequest() {
			Name = "Colchões S/A",
			CompanyReason = "Colchões",
			Telephone = "38999999999",
			Document = "12312312312",
			DocumentIsCPF = true,
			Email = "colchoes@gmail.com",
			Street = "Rua x",
			Number = "198",
			Neighborhood = "Centro",
			City = "Gotham",
			State = "New York",
			Country = "Brasil",
			ZipCode = "12312312"
		};

		await handler.Handle(request);
		Assert.AreEqual(false, handler.IsValid);
	}

	[TestMethod]
	public async Task UpdateShouldReturnSuccessWhenDocumentNotExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository(), new FakeTransactionRepository());
		var request = new UpdateSupplierRequest() {
			Name = "Colchões S/A",
			CompanyReason = "Colchões",
			Telephone = "38999999999",
			Document = "11111111111",
			DocumentIsCPF = true,
			Email = "colchoes@gmail.com",
			Street = "Rua x",
			Number = "198",
			Neighborhood = "Centro",
			City = "Gotham",
			State = "New York",
			Country = "Brasil",
			ZipCode = "12312312"
		};

		await handler.Handle(request);
		Assert.AreEqual(true, handler.IsValid);
	}
}