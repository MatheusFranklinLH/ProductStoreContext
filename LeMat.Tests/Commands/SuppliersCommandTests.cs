using LeMat.Domain.Requests;

namespace LeMat.Tests;

[TestClass]
public class SuppliersRequestTests {

	[TestMethod]
	public void CreateShouldReturnErrorWhenNameIsInvalid() {
		var request = new CreateSupplierRequest();
		request.Name = "";

		request.Validate();
		Assert.AreEqual(false, request.IsValid);
	}

	[TestMethod]
	public void UpdateShouldReturnErrorWhenNameIsInvalid() {
		var request = new UpdateSupplierRequest();
		request.Name = "";

		request.Validate();
		Assert.AreEqual(false, request.IsValid);
	}
}