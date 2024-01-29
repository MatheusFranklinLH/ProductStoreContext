using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Domain.ValueObjects;

namespace StoreProductsContext.Tests.Mocks;

public class FakeEntitiesRepository {
	//VOs
	private readonly Name _name;
	private readonly Telephone _telephone;
	private readonly Email _email;
	private readonly Address _address;
	private readonly Document _document;
	public FakeEntitiesRepository() {
		_name = new("Bruce", "Wayne");
		_telephone = new("(55) 99999-7620");
		_email = new("123@gmail.com");
		_address = new("1 street", "123", "Santa Cruz", "Gotham", "Massachussets", "EUA", "12313213");
		_document = new("12312312312", EDocumentType.CPF);
	}

	public Product GetValidProduct() {
		Supplier supplier = new("Fornecedor 1", "Fornecedor 1 S/A", _telephone, _email, _address, _document);
		return new("Produto 1", 100, supplier);
	}

	public Sales GetValidSales() {
		Client client = new(_name, _telephone, _email, _address, _document);
		return new(200, 20, 180, DateTime.UtcNow.AddDays(-1), client);
	}

	public SalesItem GetValidSalesItem() {
		return new(GetValidProduct(), 5, 20);
	}
}