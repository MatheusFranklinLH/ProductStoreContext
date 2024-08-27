using LeMat.Domain.Commands;

namespace LeMat.Tests;

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