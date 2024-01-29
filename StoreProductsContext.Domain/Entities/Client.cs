using Flunt.Validations;
using StoreProductsContext.Domain.ValueObjects;
using StoreProductsContext.Shared.Entities;

namespace StoreProductsContext.Domain.Entities;

public class Client : ContactEntity {
	public Client(Name name, Telephone telephone, Email email, Address address, Document document)
		: base(telephone, email, address, document) {
		Name = name;

		AddNotifications(Name);
	}

	public Name Name { get; private set; }

	public override string ToString() {
		return Name.ToString();
	}
}