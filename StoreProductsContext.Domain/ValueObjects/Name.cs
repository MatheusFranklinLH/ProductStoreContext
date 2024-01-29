using Flunt.Validations;
using StoreProductsContext.Shared.ValueObjects;

namespace StoreProductsContext.Domain.ValueObjects;

public class Name : ValueObject {
	public Name(string firstName, string lastName) {
		FirstName = firstName;
		LastName = lastName;

		AddNotifications(new Contract<string>()
			.Requires()
			.IsGreaterThan(FirstName, 3, "Name.FirstName", "Nome deve conter no minimo 3 caracteres")
			.IsGreaterThan(LastName, 3, "Name.LastName", "Sobrenome deve conter no minimo 3 caracteres")
			.IsLowerThan(FirstName, 40, "Name.FirstName", "Nome deve conter no máximo 40 caracteres")
			.IsLowerThan(LastName, 40, "Name.LastName", "Sobrenome deve conter no máximo 40 caracteres")
		);
	}

	public string FirstName { get; private set; }
	public string LastName { get; private set; }

	public override string ToString() {
		return $"{FirstName} {LastName}";
	}
}