using StoreProductsContext.Domain.Commands;
using StoreProductsContext.Domain.Handlers;
using StoreProductsContext.Tests.Mocks;

namespace StoreProductsContext.Tests;

[TestClass]
public class SupplierHandlerTests {
	[TestMethod]
	public void ShouldReturnErrorWhenDocumentExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository());
		var command = new CreateSupplierCommand() {
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

		handler.Handle(command);
		Assert.AreEqual(false, handler.IsValid);
	}

	[TestMethod]
	public void ShouldReturnSuccessWhenDocumentNotExists() {
		var handler = new SupplierHandler(new FakeSupplierRepository());
		var command = new CreateSupplierCommand() {
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

		handler.Handle(command);
		Assert.AreEqual(true, handler.IsValid);
	}
}