using Flunt.Notifications;
using LeMat.Domain.Entities;
using LeMat.Domain.Repositories;
using LeMat.Domain.Requests;
using LeMat.Domain.Responses;
using LeMat.Shared.Handlers;
using LeMat.Shared.Requests;
using LeMat.Shared.Responses;

namespace LeMat.Domain.Handlers;

public class ProductImageHandler :
	Notifiable<Notification>,
	IHandler<UpdateProductImageRequest>,
	IHandler<DeleteIdRequest> {
	private readonly IProductImageRepository _repository;
	private readonly IFilesRepository _filesRepository;

	public ProductImageHandler(
		IProductImageRepository repository,
		IFilesRepository filesRepository
	) {
		_repository = repository;
		_filesRepository = filesRepository;
	}

	public async Task<IResponse> Handle(int productId) {
		var product = await _repository.GetProductByIdWithImagesAsync(productId);
		if (product is null)
			return new Response(null, 400, "Impossível encontrar produto!");

		List<ImageResponse> images = new();
		foreach (var image in product.Images) {
			string imageBase64 = null;
			try {
				imageBase64 = await _filesRepository.GetFileAsBase64Async(image.ImageName);
			}
			catch { }
			imageBase64 = "data:image/jpeg;base64," + imageBase64;
			if (imageBase64 is not null)
				images.Add(new(image.Id, imageBase64));
		}

		return new Response(images);
	}

	public async Task<IResponse> Handle(UpdateProductImageRequest request) {
		request.Validate();
		if (!request.IsValid)
			return new Response(request.Notifications, 400, "Parâmetros de entrada inválidos!");

		Image newImage;
		try {
			string newFileName = await _filesRepository.UploadImageAsync(request.Image, new() { ".jpeg", ".jpg", ".png" });
			newImage = new(newFileName, request.ProductId);
		}
		catch (ArgumentException ae) {
			return new Response(null, 500, ae.Message);
		}
		catch {
			return new Response(null, 500, "Erro ao tentar salvar imagem!");
		}

		AddNotifications(newImage);

		if (!IsValid)
			return new Response(Notifications, 400, "Impossível fazer upload de imagem do produto!");
		try {
			await _repository.CreateAsync(newImage);
		}
		catch {
			return new Response(null, 500, "Não foi atualizar criar produto!");
		}

		return new Response(null, 200, "Imagens atualizadas com sucesso!");
	}

	public async Task<IResponse> Handle(DeleteIdRequest request) {
		var image = await _repository.GetByIdAsync(request.Id);
		if (image is null)
			return new Response(null, 400, "Impossível encontrar imagem!");

		try {
			await _filesRepository.DeleteFileAsync(image.ImageName);
		}
		catch (FileNotFoundException) { }
		catch {
			return new Response(null, 500, "Erro ao tentar remover imagem!");
		}

		try {
			await _repository.DeleteAsync(image);
		}
		catch {
			return new Response(null, 500, "Não foi possível remover produto!");
		}
		return new Response(null, 200, "Produto removido com sucesso!");
	}
}