using LeMat.Domain.Commands;

namespace LeMat.Tests;

[TestClass]
public class SuppliersCommandTests {

	[TestMethod]
	public void CreateShouldReturnErrorWhenNameIsInvalid() {
		var command = new CreateSupplierCommand();
		command.Name = "";

		command.Validate();
		Assert.AreEqual(false, command.IsValid);
	}

	[TestMethod]
	public void UpdateShouldReturnErrorWhenNameIsInvalid() {
		var command = new UpdateSupplierCommand();
		command.Name = "";

		command.Validate();
		Assert.AreEqual(false, command.IsValid);
	}
}