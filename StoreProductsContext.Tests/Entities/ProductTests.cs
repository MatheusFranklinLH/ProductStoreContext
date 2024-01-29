using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Domain.ValueObjects;
using StoreProductsContext.Tests.Mocks;

namespace StoreProductsContext.Tests;

[TestClass]
public class ProductTests {
	private readonly Product _product;
	public ProductTests() {
		_product = new FakeEntitiesRepository().GetValidProduct();
	}
	[TestMethod]
	public void ShouldReturnErrorWhenAddZeroQuantityToStock() {
		_product.AddToStock(0);
		Assert.IsFalse(_product.IsValid);
	}

	[TestMethod]
	public void ShouldReturnSuccessWhenAddTenQuantityToStock() {
		_product.AddToStock(10);
		Assert.IsTrue(_product.IsValid);
	}

	[TestMethod]
	public void ShouldReturnErrorWhenRemoveZeroQuantityFromStock() {
		_product.RemoveFromStock(0);
		Assert.IsFalse(_product.IsValid);
	}

	[TestMethod]
	public void ShouldReturnSuccessWhenRemoveTenQuantityFromStock() {
		_product.RemoveFromStock(10);
		Assert.IsTrue(_product.IsValid);
	}

	[TestMethod]
	public void ShouldReturnErrorWhenAddNullInventoryControl() {
		_product.AddInvetoryControl(null);
		Assert.IsFalse(_product.IsValid);
	}

	[TestMethod]
	public void ShouldReturnSuccessWhenAddValidInventoryControl() {
		var inventoryControl = new InventoryControl(2, true, DateTime.UtcNow.AddDays(-1), 100);
		_product.AddInvetoryControl(inventoryControl);
		Assert.IsTrue(_product.IsValid);
	}
}