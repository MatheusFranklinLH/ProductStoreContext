using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class InventoryControl : Entity {
	public InventoryControl(int quantity, bool @out, DateTime date, decimal totalValue) {
		Quantity = quantity;
		Out = @out;
		Date = date;
		TotalValue = totalValue;

		AddNotifications(new Contract<InventoryControl>()
			.Requires()
			.IsGreaterThan(Quantity, 0, "InventoryControl.Quantity", "Quantidade de um controle de estoque deve ser maior que 0")
			.IsLowerOrEqualsThan(Date, DateTime.UtcNow, "InventoryControl.Date", "Impossível fazer controle de estoque futuro")
			.IsGreaterOrEqualsThan(Quantity, 0, "InventoryControl.TotalValue", "Valor de um controle de estoque deve ser maior ou igual a 0")
		);
	}

	public int Quantity { get; private set; }
	public bool Out { get; private set; }
	public DateTime Date { get; private set; }
	public decimal TotalValue { get; private set; }

}