using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class Image : Entity {
	private Image() { }
	public Image(string imagePath, int productId) {
		ImagePath = imagePath;
		ProductId = productId;

		Validate();
	}
	public string ImagePath { get; private set; }
	public int ProductId { get; private set; }
	public virtual Product Product { get; private set; }

	private void Validate() {
		AddNotifications(new Contract<Product>()
			.Requires()
			.IsNotNull(ProductId, "Image.ProductId", "ID do produto não pode ser nulo")
		);
	}
}