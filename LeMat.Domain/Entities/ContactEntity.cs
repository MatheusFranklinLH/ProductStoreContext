using LeMat.Domain.ValueObjects;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public abstract class ContactEntity : Entity {
	protected ContactEntity() { }
	protected ContactEntity(Telephone telephone, Email email, Address address, Document document) {
		Telephone = telephone;
		Email = email;
		Address = address;
		Document = document;

		if (Telephone is not null)
			AddNotifications(Telephone);
		if (Email is not null)
			AddNotifications(Email);
		if (Address is not null)
			AddNotifications(Address);
		if (Document is not null)
			AddNotifications(Document);
	}

	public Telephone Telephone { get; private set; }
	public Email Email { get; private set; }
	public Address Address { get; private set; }
	public Document Document { get; private set; }
}