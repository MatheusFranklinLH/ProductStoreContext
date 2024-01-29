using Flunt.Validations;
using StoreProductsContext.Shared.ValueObjects;

namespace StoreProductsContext.Domain.ValueObjects;

public class Address : ValueObject {
	public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode) {
		Street = street;
		Number = number;
		Neighborhood = neighborhood;
		City = city;
		State = state;
		Country = country;
		ZipCode = zipCode;

		AddNotifications(new Contract<string>()
			.Requires()
			.IsGreaterThan(Street.Length, 3, "Address.Street", "Rua deve conter no minimo 3 caracteres")
		);
	}

	public string Street { get; private set; }
	public string Number { get; private set; }
	public string Neighborhood { get; private set; }
	public string City { get; private set; }
	public string State { get; private set; }
	public string Country { get; private set; }
	public string ZipCode { get; private set; }
}