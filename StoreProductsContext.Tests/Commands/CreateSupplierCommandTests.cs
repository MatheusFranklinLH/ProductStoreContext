using StoreProductsContext.Domain.Commands;

namespace StoreProductsContext.Tests;

[TestClass]
public class CreateSupplierCommandTests {

	[TestMethod]
	public void ShouldReturnErrorWhenNameIsInvalid() {
		var command = new CreateSupplierCommand();
		command.Name = "";

		command.Validate();
		Assert.AreEqual(false, command.IsValid);
	}
}