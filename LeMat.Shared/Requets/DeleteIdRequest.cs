using Flunt.Notifications;
using Flunt.Validations;

namespace LeMat.Shared.Requests;

public class DeleteIdRequest : Notifiable<Notification>, IRequest {
	public int Id { get; set; }
	public void Validate() {
		AddNotifications(new Contract<DeleteIdRequest>()
			.Requires()
			.IsGreaterThan(Id, 0, "DeleteIdRequest.Id", "ID inválido")
		);
	}
}