using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class StockEntryItem : Entity {
	private StockEntryItem() { }
	public StockEntryItem(int quantity, decimal costPerUnit, int productId, int stockEntryId) {
		Quantity = quantity;
		CostPerUnit = costPerUnit;
		ProductId = productId;
		StockEntryId = stockEntryId;

		Validate();
	}

	private void Validate() {
		AddNotifications(new Contract<StockEntryItem>()
			.Requires()
			.IsGreaterThan(Quantity, 0, "StockEntryItem.Quantity", "Quantidade de uma entrade de estoque de um produto deve ser maior que 0")
			.IsGreaterOrEqualsThan(CostPerUnit, 0, "StockEntryItem.CostPerUnit", "Valor por unidade de um produto deve ser maior ou igual a 0")
		);
	}

	public int Quantity { get; private set; }
	public decimal CostPerUnit { get; private set; }
	public int ProductId { get; set; }
	public int StockEntryId { get; set; }
	public virtual Product Product { get; set; }
	public virtual StockEntry StockEntry { get; set; }

}