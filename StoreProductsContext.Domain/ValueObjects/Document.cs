using Flunt.Validations;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Shared.ValueObjects;

namespace StoreProductsContext.Domain.ValueObjects;

public class Document : ValueObject {

	public Document(string number, Enums.EDocumentType type) {
		Number = number;
		Type = type;

		AddNotifications(new Contract<string>()
			.Requires()
			.IsTrue(Validate(), "Document.Number", "Documento inválido")
		);
	}
	public string Number { get; private set; }
	public EDocumentType Type { get; private set; }

	private bool Validate() {
		if (Type == EDocumentType.CNPJ && Number.Length == 14)
			return true;
		if (Type == EDocumentType.CPF && Number.Length == 11)
			return true;
		return false;
	}
}