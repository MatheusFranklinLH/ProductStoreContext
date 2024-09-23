using Flunt.Validations;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Entities;

public class Client : ContactEntity {
	private Client() { }
	public Client(Name name, Telephone telephone, Email email, Address address, Document document)
		: base(telephone, email, address, document) {
		Name = name;

		AddNotifications(Name, new Contract<Client>()
			.Requires()
			.IsNotNull(Name, "Client.Name", "Nome do cliente não pode ser nulo!")
			.IsNotNull(Document, "Client.Document", "Documento do cliente não pode ser nulo!")
		);
	}

	public Name Name { get; private set; }

	public override string ToString() {
		return Name.ToString();
	}
}