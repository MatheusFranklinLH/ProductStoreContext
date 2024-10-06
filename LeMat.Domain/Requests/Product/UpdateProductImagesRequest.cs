using Flunt.Notifications;
using Flunt.Validations;
using LeMat.Shared.Requests;
using Microsoft.AspNetCore.Http;

namespace LeMat.Domain.Requests;

public class UpdateProductImagesRequest : Notifiable<Notification>, IRequest {
	public int ProductId { get; set; }
	public List<IFormFile> Images { get; set; }
	public void Validate() {
		AddNotifications(new Contract<UpdateProductImagesRequest>()
			.Requires()
			.IsGreaterThan(ProductId, 0, "ProductId", "O ID do produto deve ser maior do que 0")
			.IsNotNull(Images, "Images", "Nenhuma imagem foi enviada")
			.IsGreaterThan(Images.Count, 0, "Images", "Nenhuma imagem foi enviada")
		);
	}
}