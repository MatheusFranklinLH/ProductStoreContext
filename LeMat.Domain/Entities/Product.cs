using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class Product : Entity {
	public Product(string name, decimal sugestedSellPrice, decimal maximumDiscountPercentage, string imagePath, int? supplierId) {
		Name = name;
		SugestedSellPrice = sugestedSellPrice;
		MaximumDiscountPercentage = maximumDiscountPercentage;
		ImagePath = imagePath;
		StockAvailable = 0;
		SupplierId = supplierId;

		AddNotifications(new Contract<Product>()
			.Requires()
			.IsGreaterThan(Name, 3, "Product.Name", "Nome deve ter mais do que 3 caracteres")
			.IsGreaterThan(SugestedSellPrice, 0, "Product.SellPrice", "Valor de venda do produto deve ser maior que 0")
			.IsGreaterOrEqualsThan(MaximumDiscountPercentage, 0, "Product.MaximumDiscountPercentage", "A porcentagem máxima de desconto deve ser maior ou igual que 0")
			.IsLowerOrEqualsThan(MaximumDiscountPercentage, 100, "Product.MaximumDiscountPercentage", "A porcentagem máxima de desconto deve ser menor ou igual que 100")
		);
	}

	public string Name { get; private set; }
	public decimal SugestedSellPrice { get; private set; }
	public decimal MaximumDiscountPercentage { get; private set; }
	public string ImagePath { get; private set; }
	public int? SupplierId { get; private set; }
	public int StockAvailable { get; private set; }
	public virtual Supplier Supplier { get; private set; }
	public virtual ICollection<StockEntry> StockEntries { get; private set; } = new HashSet<StockEntry>();
	public virtual ICollection<Sales> Sales { get; private set; } = new HashSet<Sales>();

	public override string ToString() {
		return Name;
	}

	public void AddToStock(int quantity) {
		if (quantity <= 0)
			AddNotification("Product.AddToStock.Quantity", "Quantidade a ser adicionada deve ser maior que 0");

		StockAvailable += quantity;
	}

	public void RemoveFromStock(int quantity) {
		if (quantity <= 0)
			AddNotification("Product.RemoveFromStock.Quantity", "Quantidade a ser removida deve ser maior que 0");

		StockAvailable -= quantity;
	}
}