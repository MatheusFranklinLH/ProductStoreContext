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
	IHandler<CreateSupplierCommand>,
	IHandler<UpdateSupplierCommand>,
	IHandler<DeleteIdCommand> {
	private readonly ISupplierRepository _repository;

	public SupplierHandler(ISupplierRepository repository) {
		_repository = repository;
	}

	public async Task<ICommandResult> Handle(CreateSupplierCommand command) {
		// Fail Fast Validate
		command.Validate();
		if (!command.IsValid)
			return new CommandResult(command.Notifications, 400, "Parâmetros de entrada inválidos!");

		Telephone telephone = GetTelephone(command.Telephone);
		Email email = GetEmail(command.Email);
		Address address = GetAddress(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
		Document document = await GetDocumentAsync(command.Document, command.DocumentIsCPF);

		var supplier = new Supplier(command.Name, command.CompanyReason, telephone, email, address, document);
		AddNotifications(supplier);

		if (!IsValid)
			return new CommandResult(Notifications, 400, "Impossível criar fornecedor!");

		await _repository.CreateAsync(supplier);

		return new CommandResult(supplier.Id, 200, "Fornecedor criado com sucesso!");
	}

	public async Task<ICommandResult> Handle(UpdateSupplierCommand command) {
		command.Validate();
		if (!command.IsValid)
			return new CommandResult(command.Notifications, 400, "Parâmetros de entrada inválidos!");

		var supplier = await _repository.GetByIdAsync(command.Id);
		if (supplier is null)
			return new CommandResult(null, 400, "Impossível encontrar fornecedor!");

		Telephone telephone = GetTelephone(command.Telephone);
		Email email = GetEmail(command.Email);
		Address address = GetAddress(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
		Document document = await GetDocumentAsync(command.Document, command.DocumentIsCPF);
		supplier.Update(command.Name, command.CompanyReason, telephone, email, address, document);
		AddNotifications(supplier);

		if (!IsValid)
			return new CommandResult(Notifications, 400, "Impossível atualizar fornecedor!");

		await _repository.UpdateAsync(supplier);

		return new CommandResult(supplier.Id, 200, "Fornecedor atualizado com sucesso!");
	}

	public async Task<ICommandResult> Handle(DeleteIdCommand command) {
		var supplier = await _repository.GetByIdAsync(command.Id);
		if (supplier is null)
			return new CommandResult(null, 400, "Impossível encontrar fornecedor!");

		await _repository.DeleteAsync(supplier);
		return new CommandResult(null, 200, "Fornecedor removido com sucesso!");
	}

	private Telephone GetTelephone(string number) {
		if (string.IsNullOrWhiteSpace(number))
			return null;
		var telephone = new Telephone(number);
		AddNotifications(telephone);
		return telephone;
	}

	private Email GetEmail(string address) {
		if (string.IsNullOrWhiteSpace(address))
			return null;
		var email = new Email(address);
		AddNotifications(email);
		return email;
	}

	private Address GetAddress(string street, string number, string neighborhood, string city, string state, string country, string zipCode) {
		if (string.IsNullOrWhiteSpace(street))
			return null;
		var address = new Address(street, number, neighborhood, city, state, country, zipCode);
		AddNotifications(address);
		return address;
	}

	private async Task<Document> GetDocumentAsync(string document, bool documentIsCPF) {
		if (string.IsNullOrWhiteSpace(document))
			return null;
		var documentType = documentIsCPF ? EDocumentType.CPF : EDocumentType.CNPJ;
		var doc = new Document(document, documentType);
		if (await _repository.DocumentExistsAsync(doc))
			AddNotification("CreateSupplierCommand.Document", "Este Documento já foi cadastrado por outro fornecedor");
		AddNotifications(doc);
		return doc;

	}

}