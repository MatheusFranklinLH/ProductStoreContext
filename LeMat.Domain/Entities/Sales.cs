using Flunt.Validations;
using LeMat.Domain.Enums;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class Sales : Entity {

	private readonly IList<SalesItem> _salesItems;
	public Sales(DateTime date, EPaymentType paymentType, Client client, DateTime? deliveryDate, List<SalesItem> salesItems) {
		Date = date;
		PaymentType = paymentType;
		Client = client;
		ClientId = client.Id;
		DeliveryDate = deliveryDate;
		_salesItems = salesItems ?? new();

		AddNotifications(Client, new Contract<Sales>()
			.Requires()
			.IsNotNull(Client, "Sales.Client", "Cliente não pode ser nulo")
		);

		foreach (var salesItem in _salesItems)
			AddNotifications(salesItem);
	}

	public int ClientId { get; private set; }
	public EPaymentType PaymentType { get; private set; }
	public DateTime Date { get; private set; }
	public DateTime? DeliveryDate { get; private set; }
	public virtual Client Client { get; private set; }
	public virtual ICollection<SalesItem> SalesItems { get; private set; }

	public void AddSalesItem(SalesItem item) {
		if (item is null) {
			AddNotification("Sales.AddSalesItem", "Impossível adicionar item nulo na venda");
			return;
		}

		AddNotifications(item);
		_salesItems.Add(item);
	}

	public decimal GetTotalPrice() {
		decimal totalPrice = 0;
		foreach (var salesItem in _salesItems)
			totalPrice += salesItem.Quantity * salesItem.PerUnitPrice;
		return totalPrice;
	}
}