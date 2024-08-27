using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Domain.Queries;

namespace StoreProductsContext.Tests;

[TestClass]
public class SupplierQueriesTests {
	private IList<Supplier> _suppliers;

	public SupplierQueriesTests() {
		_suppliers = new List<Supplier>() {
			new Supplier("Supplier1", "Sup1", new("12345678912"), new("supplier1@email.com"), new("Rua x", "900", "Bairro", "Cidade", "Estado", "País", "1231241241"), new("12312312312", EDocumentType.CPF)),
			new Supplier("Supplier2", "Sup2", new("12345678912"), new("supplier2@email.com"), new("Rua x", "900", "Bairro", "Cidade", "Estado", "País", "1231241241"), new("12312312312", EDocumentType.CPF)),
		};
	}

	[TestMethod]
	public void ShouldReturnNullWhenSupplierNotExists() {
		var id = 1;
		var exp = SupplierQueries.GetSupplier(id);
		var supplier = _suppliers.AsQueryable().Where(exp).FirstOrDefault();

		Assert.AreEqual(null, supplier);
	}

	[TestMethod]
	public void ShouldReturnSupplierWhenSupplierExists() {
		var supplier = new Supplier("Supplier3", "Sup3", new("12345678912"), new("supplier1@email.com"), new("Rua x", "900", "Bairro", "Cidade", "Estado", "País", "1231241241"), new("12312312312", EDocumentType.CPF));
		supplier.Id = 2;
		var exp = SupplierQueries.GetSupplier(2);
		_suppliers.Add(supplier);
		var foundSupplier = _suppliers.AsQueryable().Where(exp).FirstOrDefault();

		Assert.AreEqual(supplier, foundSupplier);
	}
}