using Flunt.Validations;
using LeMat.Domain.ValueObjects;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

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