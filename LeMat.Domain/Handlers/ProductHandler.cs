using Flunt.Notifications;
using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Domain.ValueObjects;
using LeMat.Shared.Handlers;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;
using LeMat.Shared.Utils;

namespace LeMat.Domain.Handlers;

public class ProductHandler :
	Notifiable<Notification>,
	IHandler<CreateProductRequest>,
	IHandler<UpdateProductRequest>,
	IHandler<DeleteIdRequest> {
	private readonly IProductRepository _repository;
	private readonly ITransactionRepository _transactionRepository;

	public ProductHandler(IProductRepository repository, ITransactionRepository transactionRepository) {
		_repository = repository;
		_transactionRepository = transactionRepository;
	}

	public async Task<IResponse> Handle(CreateProductRequest request) {
		// Fail Fast Validate
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");
		string imagePath = "";
		if (request.Image is not null)
			try {
				imagePath = await FileUtils.UploadFileAsync(request.Image, new() { ".jpg", ".png" });
			}
			catch (ArgumentException ae) {
				return new Response(null, 500, ae.Message);
			}
			catch {
				return new Response(null, 500, "Erro ao tentar salvar imagem!");
			}

		Product product = new(request.Name, request.SuggestedSellPrice, request.MaximumDiscountPercentage, imagePath, request.SupplierId);
		AddNotifications(product);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível criar produto!");
		try {
			await _repository.CreateAsync(product);
		}
		catch {
			return new Response(null, 500, "Não foi possível criar produto!");
		}

		return new Response(product.Id, 200, "Produto criado com sucesso!");
	}

	public async Task<IResponse> Handle(UpdateProductRequest request) {
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		var product = await _repository.GetByIdAsync(request.Id);
		if (product is null)
			return new Response(null, 400, "Impossível encontrar produto!");

		if (request.ImageHasBeenChanged) {
			if (!string.IsNullOrWhiteSpace(product.ImagePath))
				FileUtils.DeleteFile(product.ImagePath);
			string imagePath = null;
			if (request.Image is not null) {
				try {
					imagePath = await FileUtils.UploadFileAsync(request.Image, new() { ".jpg", ".png" });
				}
				catch (ArgumentException ae) {
					return new Response(null, 500, ae.Message);
				}
				catch {
					return new Response(null, 500, "Erro ao tentar salvar imagem!");
				}
			}
			product.UpdateImagePath(imagePath);
		}

		product.Update(request.Name, request.SuggestedSellPrice, request.MaximumDiscountPercentage, request.SupplierId);
		AddNotifications(product);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível atualizar produto!");
		await _transactionRepository.BeginTransactionAsync();
		try {
			try {
				await _repository.UpdateAsync(product);
			}
			catch {
				return new Response(null, 500, "Não foi atualizar criar produto!");
			}
			await _transactionRepository.CommitAsync();
		}
		catch (Exception) {
			await _transactionRepository.RollbackAsync();
			throw;
		}

		return new Response(product.Id, 200, "Produto atualizado com sucesso!");
	}

	public async Task<IResponse> Handle(DeleteIdRequest request) {
		var product = await _repository.GetByIdAsync(request.Id);
		if (product is null)
			return new Response(null, 400, "Impossível encontrar produto!");

		try {
			await _repository.DeleteAsync(product);
		}
		catch {
			return new Response(null, 500, "Não foi possível remover produto!");
		}
		return new Response(null, 200, "Produto removido com sucesso!");
	}

}