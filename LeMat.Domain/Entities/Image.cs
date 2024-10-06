using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class Image : Entity {
	private Image() { }
	public Image(string imageName, int productId) {
		ImageName = imageName;
		ProductId = productId;

		Validate();
	}
	public string ImageName { get; private set; }
	public int ProductId { get; private set; }
	public virtual Product Product { get; private set; }

	private void Validate() {
		AddNotifications(new Contract<Product>()
			.Requires()
			.IsNotNull(ProductId, "Image.ProductId", "ID do produto não pode ser nulo")
			.IsGreaterThan(ProductId, 0, "Image.ProductId", "ID do produto deve ser maior do que 0")
		);
	}
}