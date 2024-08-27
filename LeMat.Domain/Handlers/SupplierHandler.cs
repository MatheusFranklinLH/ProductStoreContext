using Flunt.Notifications;
using LeMat.Domain.Commands;
using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.ValueObjects;
using LeMat.Shared.Commands;
using LeMat.Shared.Handlers;

namespace LeMat.Domain.Handlers;

public class SupplierHandler :
	Notifiable<Notification>,
	IHandler<CreateSupplierCommand> {
	private readonly ISupplierRepository _repository;

	public SupplierHandler(ISupplierRepository repository) {
		_repository = repository;
	}

	public async Task<ICommandResult> Handle(CreateSupplierCommand command) {
		// Fail Fast Validate
		command.Validate();
		if (!command.IsValid)
			return new CommandResult(command.Notifications, 400, "Parâmetros de entrada inválidos!");

		Telephone telephone = null;
		if (!string.IsNullOrWhiteSpace(command.Telephone)) {
			telephone = new Telephone(command.Telephone);
			AddNotifications(telephone);
		}
		Email email = null;
		if (!string.IsNullOrWhiteSpace(command.Email)) {
			email = new Email(command.Email);
			AddNotifications(email);
		}
		Address address = null;
		if (!string.IsNullOrWhiteSpace(command.Street)) {
			address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
			AddNotifications(address);
		}

		Document document = null;
		if (!string.IsNullOrWhiteSpace(command.Document)) {
			var documentType = command.DocumentIsCPF ? EDocumentType.CPF : EDocumentType.CNPJ;
			document = new Document(command.Document, documentType);

			if (await _repository.DocumentExistsAsync(document))
				AddNotification("CreateSupplierCommand.Document", "Este Documento já foi cadastrado por outro fornecedor");
			AddNotifications(document);
		}

		var supplier = new Supplier(command.Name, command.CompanyReason, telephone, email, address, document);
		AddNotifications(supplier);

		if (!IsValid)
			return new CommandResult(Notifications, 400, "Impossível criar fornecedor!");

		await _repository.CreateAsync(supplier);

		return new CommandResult(supplier.Id, 200, "Fornecedor criado com sucesso!");
	}
}