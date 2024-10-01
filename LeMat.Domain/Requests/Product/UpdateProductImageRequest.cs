using Flunt.Notifications;
using Flunt.Validations;
using LeMat.Shared.Requests;
using Microsoft.AspNetCore.Http;

namespace LeMat.Domain.Requests;

public class UpdateProductImageRequest : Notifiable<Notification>, IRequest {
	public int ProductId { get; set; }
	public IFormFile Image { get; set; }
	public void Validate() {
		AddNotifications(new Contract<UpdateProductImageRequest>()
			.Requires()
			.IsGreaterThan(ProductId, 0, "ProductId", "O ID do produto deve ser maior do que 0")
			.IsNotNull(Image, "Image", "Imagem inválida")
		);
	}
}