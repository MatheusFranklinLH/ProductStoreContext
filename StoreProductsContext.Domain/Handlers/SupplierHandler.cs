using Flunt.Notifications;
using StoreProductsContext.Domain.Commands;
using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;
using StoreProductsContext.Domain.Repositories;
using StoreProductsContext.Domain.ValueObjects;
using StoreProductsContext.Shared.Commands;
using StoreProductsContext.Shared.Handlers;

namespace StoreProductsContext.Domain.Handlers;

public class SupplierHandler :
	Notifiable<Notification>,
	IHandler<CreateSupplierCommand> {
	private readonly ISupplierRepository _repository;

	public SupplierHandler(ISupplierRepository repository) {
		_repository = repository;
	}

	public ICommandResult Handle(CreateSupplierCommand command) {
		// Fail Fast Validate
		command.Validate();
		if (!command.IsValid)
			return new GenericCommandResult(false, "Parâmetros de entrada inválidos!", command.Notifications);

		// Gerar os VOs
		var telephone = new Telephone(command.Telephone);
		var email = new Email(command.Email);
		var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
		var documentType = command.DocumentIsCPF ? EDocumentType.CPF : EDocumentType.CNPJ;
		var document = new Document(command.Document, documentType);

		//Validar Documento
		if (_repository.DocumentExists(document))
			AddNotification("CreateSupplierCommand.Document", "Este Documento já foi cadastrado por outro fornecedor");


		// Gerar o Supplier
		var supplier = new Supplier(command.Name, command.CompanyReason, telephone, email, address, document);

		AddNotifications(telephone, email, address, document, supplier);
		if (!IsValid)
			return new GenericCommandResult(false, "Impossível criar fornecedor!", Notifications);

		_repository.Create(supplier);

		return new GenericCommandResult(true, "Fornecedor criado com sucesso!", supplier.Id);
	}
}