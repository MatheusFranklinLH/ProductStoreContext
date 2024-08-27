using Flunt.Validations;
using LeMat.Domain.ValueObjects;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class Supplier : ContactEntity {

	public Supplier(string name) : base() {
		Name = name;

		AddNotifications(new Contract<Supplier>()
			.Requires()
			.IsGreaterThan(Name, 3, "Supplier.Name", "Nome deve ter mais do que 3 caracteres")
		);
	}

	public Supplier(string name, string companyReason, Telephone telephone, Email email, Address address, Document document)
		: base(telephone, email, address, document) {
		Name = name;
		CompanyReason = companyReason;

		AddNotifications(new Contract<Supplier>()
			.Requires()
			.IsGreaterThan(Name, 3, "Supplier.Name", "Nome deve ter mais do que 3 caracteres")
		);
	}
	public string Name { get; private set; }
	public string CompanyReason { get; private set; }
}