using Flunt.Validations;
using LeMat.Shared.ValueObjects;

namespace LeMat.Domain.ValueObjects;

public class Email : ValueObject {
	public Email(string address) {
		Address = address;

		AddNotifications(new Contract<string>()
			.Requires()
			.IsEmail(Address, "Email.Address", "Email inválido")
		);
	}

	public string Address { get; private set; }

	public override string ToString() {
		return Address;
	}
}