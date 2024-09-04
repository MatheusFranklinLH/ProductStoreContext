using Flunt.Notifications;
using Flunt.Validations;

namespace LeMat.Shared.Commands;

public class DeleteIdCommand : Notifiable<Notification>, ICommand {
	public int Id { get; set; }
	public void Validate() {
		AddNotifications(new Contract<DeleteIdCommand>()
			.Requires()
			.IsGreaterThan(Id, 0, "DeleteIdCommand.Id", "ID inválido")
		);
	}
}