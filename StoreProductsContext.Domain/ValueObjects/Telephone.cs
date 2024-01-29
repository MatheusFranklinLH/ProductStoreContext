using Flunt.Validations;
using StoreProductsContext.Shared.ValueObjects;

namespace StoreProductsContext.Domain.ValueObjects;

public class Telephone : ValueObject {
	public Telephone(string number) {
		Number = OnlyDigits(number);

		AddNotifications(new Contract<string>()
			.Requires()
			.IsGreaterOrEqualsThan(Number, 10, "Telephone.Number", "Telefone deve conter 10 ou 11 números")
			.IsLowerOrEqualsThan(Number, 11, "Telephone.Number", "Telefone deve conter 10 ou 11 números")
		);
	}

	public string Number { get; private set; }

	public override string ToString() {
		return Number;
	}

	private string OnlyDigits(string number) {
		return new string(number.Where(char.IsDigit).ToArray());
	}
}