using Flunt.Notifications;
using LeMat.Shared.Commands;

namespace LeMat.Shared.Commands;

public class DeleteIdCommand : Notifiable<Notification>, ICommand {
	public int Id { get; set; }
	public void Validate() {
	}
}