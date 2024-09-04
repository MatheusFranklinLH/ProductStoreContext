using Flunt.Notifications;
using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Domain.ValueObjects;
using LeMat.Shared.Handlers;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;

namespace LeMat.Domain.Handlers;

public class SupplierHandler :
	Notifiable<Notification>,
	IHandler<CreateSupplierRequest>,
	IHandler<UpdateSupplierRequest>,
	IHandler<DeleteIdRequest> {
	private readonly ISupplierRepository _repository;
	private readonly ITransactionRepository _transactionRepository;

	public SupplierHandler(ISupplierRepository repository, ITransactionRepository transactionRepository) {
		_repository = repository;
		_transactionRepository = transactionRepository;
	}

	public async Task<IResponse> Handle(CreateSupplierRequest request) {
		// Fail Fast Validate
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		Telephone telephone = GetTelephone(request.Telephone);
		Email email = GetEmail(request.Email);
		Address address = GetAddress(request.Street, request.Number, request.Neighborhood, request.City, request.State, request.Country, request.ZipCode);
		Document document = await GetDocumentAsync(request.Document, request.DocumentIsCPF);

		var supplier = new Supplier(request.Name, request.CompanyReason, telephone, email, address, document);
		AddNotifications(supplier);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível criar fornecedor!");

		await _repository.CreateAsync(supplier);

		return new Response(supplier.Id, 200, "Fornecedor criado com sucesso!");
	}

	public async Task<IResponse> Handle(UpdateSupplierRequest request) {
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		var supplier = await _repository.GetByIdAsync(request.Id);
		if (supplier is null)
			return new Response(null, 400, "Impossível encontrar fornecedor!");

		Telephone telephone = GetTelephone(request.Telephone);
		Email email = GetEmail(request.Email);
		Address address = GetAddress(request.Street, request.Number, request.Neighborhood, request.City, request.State, request.Country, request.ZipCode);
		Document document = await GetDocumentAsync(request.Document, request.DocumentIsCPF);
		supplier.Update(request.Name, request.CompanyReason, telephone, email, address, document);
		AddNotifications(supplier);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível atualizar fornecedor!");
		await _transactionRepository.BeginTransactionAsync();
		try {
			await _repository.UpdateAsync(supplier);
			await _transactionRepository.CommitAsync();
		}
		catch (Exception) {
			await _transactionRepository.RollbackAsync();
			throw;
		}

		return new Response(supplier.Id, 200, "Fornecedor atualizado com sucesso!");
	}

	public async Task<IResponse> Handle(DeleteIdRequest request) {
		var supplier = await _repository.GetByIdAsync(request.Id);
		if (supplier is null)
			return new Response(null, 400, "Impossível encontrar fornecedor!");

		await _repository.DeleteAsync(supplier);
		return new Response(null, 200, "Fornecedor removido com sucesso!");
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
			AddNotification("CreateSupplierRequest.Document", "Este Documento já foi cadastrado por outro fornecedor");
		AddNotifications(doc);
		return doc;

	}

}