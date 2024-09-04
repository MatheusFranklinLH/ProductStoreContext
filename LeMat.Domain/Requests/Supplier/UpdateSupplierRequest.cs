using Flunt.Notifications;
using Flunt.Validations;
using LeMat.Shared.Requests;

namespace LeMat.Domain.Requests;

public class UpdateSupplierRequest : Notifiable<Notification>, IRequest {
	public int Id { get; set; }
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
	public void Validate() {
		AddNotifications(new Contract<UpdateSupplierRequest>()
			.Requires()
			.IsGreaterThan(Name, 3, "Name", "Nome deve conter no mínimo 3 caracteres")
		);
	}
}