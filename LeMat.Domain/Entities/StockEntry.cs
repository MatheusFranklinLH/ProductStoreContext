using Flunt.Validations;
using LeMat.Shared.Entities;

namespace LeMat.Domain.Entities;

public class StockEntry : Entity {
	private IList<StockEntryItem> _stockEntryItems;
	private StockEntry() { }
	public StockEntry(DateTime arrivalDate, List<StockEntryItem> items) {
		ArrivalDate = arrivalDate;
		_stockEntryItems = items ?? new();
		foreach (var item in items) {
			AddNotifications(item);
		}
	}
	public DateTime ArrivalDate { get; private set; }
	public virtual ICollection<StockEntryItem> StockEntryItems => _stockEntryItems;
}