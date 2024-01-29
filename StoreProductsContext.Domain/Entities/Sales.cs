using Flunt.Validations;
using StoreProductsContext.Shared.Entities;

namespace StoreProductsContext.Domain.Entities;

public class Sales : Entity {
	private readonly IList<SalesItem> _salesItems;
	public Sales(decimal originalPrice, decimal discount, decimal price, DateTime date, Client client) {
		OriginalPrice = originalPrice;
		Discount = discount;
		Price = price;
		Date = date;
		Client = client;
		_salesItems = new List<SalesItem>();

		AddNotifications(Client, new Contract<Sales>()
			.Requires()
			.IsGreaterThan(OriginalPrice, 0, "Sales.OriginalPrice", "Valor original da venda deve ser maior que 0")
			.IsGreaterThan(Price, 0, "Sales.Price", "Valor da venda deve ser maior que 0")
			.IsGreaterOrEqualsThan(Discount, 0, "Sales.Discount", "Valor do desconto deve ser maior ou igual a 0")
			.IsLowerOrEqualsThan(Date, DateTime.UtcNow, "Sales.Date", "Data da venda deve ser antes de agora")
			.IsNotNull(Client, "Sales.Client", "Cliente não pode ser nulo")
		);
	}

	public decimal OriginalPrice { get; private set; }
	public decimal Discount { get; private set; }
	public decimal Price { get; private set; }
	public DateTime Date { get; private set; }
	public Client Client { get; private set; }
	public IReadOnlyCollection<SalesItem> SalesItems { get => _salesItems.ToArray(); }

	public void AddSalesItem(SalesItem item) {
		if (item is null) {
			AddNotification("Sales.AddSalesItem", "Impossível adicionar item nulo na venda");
			return;
		}

		AddNotifications(item);
		_salesItems.Add(item);
	}
}