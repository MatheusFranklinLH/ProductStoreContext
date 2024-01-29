using Flunt.Notifications;
using StoreProductsContext.Shared.Commands;

namespace StoreProductsContext.Domain.Commands;

public class CreateSupplierCommand : Notifiable<Notification>, ICommand {
	public string Name { get; set; }
	public string CompanyReason { get; set; }
	public string Telephone { get; set; }
	public string Document { get; set; }
	public bool DocumentIsCPF { get; set; }
	public string Email { get; set; }
	public string Street { get; set; }
	public string Number { get; set; }
	public string Neighborhood { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public string ZipCode { get; set; }
	public void Validate() { }
}