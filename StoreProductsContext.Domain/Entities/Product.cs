using Flunt.Validations;
using StoreProductsContext.Shared.Entities;

namespace StoreProductsContext.Domain.Entities;

public class Product : Entity {
	private readonly IList<InventoryControl> _inventories;
	public Product(string name, decimal sellPrice, Supplier supplier) {
		Name = name;
		SellPrice = sellPrice;
		StockAvailable = 0;
		Supplier = supplier;
		_inventories = new List<InventoryControl>();

		AddNotifications(Supplier);
		AddNotifications(new Contract<Product>()
			.Requires()
			.IsGreaterThan(Name, 3, "Product.Name", "Nome deve ter mais do que 3 caracteres")
			.IsGreaterThan(SellPrice, 0, "Product.SellPrice", "Valor de venda do produto deve ser maior que 0")
		);
	}

	public string Name { get; private set; }
	public decimal SellPrice { get; private set; }
	public int StockAvailable { get; private set; }
	public Supplier Supplier { get; private set; }
	public IReadOnlyCollection<InventoryControl> Inventories { get => _inventories.ToArray(); }

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

	public void AddInvetoryControl(InventoryControl inventoryControl) {
		if (inventoryControl is null) {
			AddNotification("Product.AddInvetoryControl", "Impossível adicionar inventoryControl nulo");
			return;
		}
		AddNotifications(inventoryControl);
		_inventories.Add(inventoryControl);
	}

}