using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.ValueObjects;
using LeMat.Tests.Mocks;

namespace LeMat.Tests;

[TestClass]
public class SalesTests {
	private readonly Sales _sales;
	private readonly FakeEntitiesRepository _fakeEntitiesRepository;
	public SalesTests(FakeEntitiesRepository fakeEntitiesRepository) {
		_fakeEntitiesRepository = fakeEntitiesRepository;
		_sales = new FakeEntitiesRepository().GetValidSales();
	}

	[TestMethod]
	public void ShouldReturnErrorWhenAddNullItem() {
		_sales.AddSalesItem(null);
		Assert.IsFalse(_sales.IsValid);
	}

	[TestMethod]
	public void ShouldSuccessErrorWhenAddValidItem() {
		_sales.AddSalesItem(_fakeEntitiesRepository.GetValidSalesItem());
		Assert.IsFalse(_sales.IsValid);
	}
}