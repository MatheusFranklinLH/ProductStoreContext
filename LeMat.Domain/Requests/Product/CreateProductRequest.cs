using Flunt.Notifications;
using Flunt.Validations;
using LeMat.Shared.Requests;
using Microsoft.AspNetCore.Http;

namespace LeMat.Domain.Requests;

public class CreateProductRequest : Notifiable<Notification>, IRequest {
	public string Name { get; set; }
	public decimal SuggestedSellPrice { get; set; }
	public decimal MaximumDiscountPercentage { get; set; }
	public IFormFile Image { get; set; }
	public int? SupplierId { get; set; }
	public void Validate() {
		AddNotifications(new Contract<CreateSupplierRequest>()
			.Requires()
			.IsGreaterThan(Name, 3, "Name", "Nome deve conter no mínimo 3 caracteres")
			.IsGreaterThan(SuggestedSellPrice, 0, "SuggestedSellPrice", "O preço sugerido de venda deve ser maior do que 0")
			.IsGreaterThan(MaximumDiscountPercentage, 0, "MaximumDiscountPercentage", "A porcentagem máxima de desconto deve ser maior do que 0")
			.IsLowerThan(MaximumDiscountPercentage, 100, "MaximumDiscountPercentage", "A porcentagem máxima de desconto deve ser menor do que 100")
		);
	}
}