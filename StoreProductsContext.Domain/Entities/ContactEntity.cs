using StoreProductsContext.Domain.ValueObjects;
using StoreProductsContext.Shared.Entities;

namespace StoreProductsContext.Domain.Entities;

public abstract class ContactEntity : Entity {
	protected ContactEntity(Telephone telephone, Email email, Address address, Document document) {
		Telephone = telephone;
		Email = email;
		Address = address;
		Document = document;

		AddNotifications(Telephone, Email, Address, Document);
	}

	public Telephone Telephone { get; private set; }
	public Email Email { get; private set; }
	public Address Address { get; private set; }
	public Document Document { get; private set; }
}