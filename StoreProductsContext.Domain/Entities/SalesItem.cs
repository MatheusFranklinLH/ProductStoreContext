using Flunt.Validations;
using StoreProductsContext.Shared.Entities;

namespace StoreProductsContext.Domain.Entities;

public class SalesItem : Entity {
	public SalesItem(Product product, int quantity, decimal perUnitPrice) {
		Product = product;
		Quantity = quantity;
		PerUnitPrice = perUnitPrice;

		AddNotifications(new Contract<SalesItem>()
			.Requires()
			.IsNotNull(Product, "SalesItem.Product", "Produto não pode ser null")
			.IsGreaterThan(Quantity, 0, "SalesItem.Quantity", "Quantidade do produto no item da venda deve ser maior que 0")
			.IsGreaterThan(perUnitPrice, 0, "SalesItem.PerUnitPrice", "Valor da unidadde do produto deve ser maior que 0")
		);
		AddNotifications(Product);
	}

	public Product Product { get; private set; }
	public int Quantity { get; private set; }
	public decimal PerUnitPrice { get; private set; }
}