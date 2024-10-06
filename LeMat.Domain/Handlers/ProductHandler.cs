using Flunt.Notifications;
using LeMat.Domain.Entities;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Domain.Responses;
using LeMat.Shared.Handlers;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;

namespace LeMat.Domain.Handlers;

public class ProductHandler :
	Notifiable<Notification>,
	IHandler<CreateProductRequest>,
	IHandler<UpdateProductRequest>,
	IHandler<DeleteIdRequest> {
	private readonly IProductRepository _repository;
	private readonly IProductImageRepository _productImageRepository;
	private readonly ITransactionRepository _transactionRepository;
	private readonly IFilesRepository _filesRepository;

	public ProductHandler(
		IProductRepository repository,
		ITransactionRepository transactionRepository,
		IProductImageRepository productImageRepository,
		IFilesRepository filesRepository
	) {
		_repository = repository;
		_transactionRepository = transactionRepository;
		_productImageRepository = productImageRepository;
		_filesRepository = filesRepository;
	}

	public async Task<IResponse> Handle(CreateProductRequest request) {
		// Fail Fast Validate
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		Product product = new(request.Name, request.SuggestedSellPrice, request.MaximumDiscountPercentage, request.SupplierId);
		AddNotifications(product);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível criar produto!");
		List<Image> newImages = new();

		await _transactionRepository.BeginTransactionAsync();
		try {
			await _repository.CreateAsync(product);
			foreach (var imageFile in request.Images) {
				string newFileName = await _filesRepository.UploadImageAsync(imageFile, new() { ".jpeg", ".jpg", ".png" });
				newImages.Add(new(newFileName, product.Id));
			}
			newImages.ForEach(x => AddNotifications(x));
			if (!IsValid) {
				await CreateProductRollbackAsync(newImages);
				return new Response(Notifications, 400, "Impossível associar imagens ao produto!");
			}
			await _productImageRepository.CreateManyImagesAsync(newImages);
			await _transactionRepository.CommitAsync();
		}
		catch (ArgumentException ae) {
			await CreateProductRollbackAsync(newImages);
			return new Response(null, 500, ae.Message);
		}
		catch (Exception) {
			await CreateProductRollbackAsync(newImages);
			return new Response(null, 500, "Não foi possível criar produto!");
		}
		return new Response(product.Id, 200, "Produto criado com sucesso!");
	}

	private async Task CreateProductRollbackAsync(List<Image> newImages) {
		await _transactionRepository.RollbackAsync();
		try {
			foreach (var image in newImages) {
				await _filesRepository.DeleteFileAsync(image.ImageName);
			}
		}
		catch { }
	}

	public async Task<IResponse> Handle(UpdateProductRequest request) {
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		var product = await _repository.GetByIdAsync(request.Id);
		if (product is null)
			return new Response(null, 400, "Impossível encontrar produto!");

		product.Update(request.Name, request.SuggestedSellPrice, request.MaximumDiscountPercentage, request.SupplierId);
		AddNotifications(product);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível atualizar produto!");

		try {
			await _repository.UpdateAsync(product);
		}
		catch {
			return new Response(null, 500, "Não foi atualizar criar produto!");
		}

		return new Response(product.MapToProductResponse(), 200, "Produto atualizado com sucesso!");
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