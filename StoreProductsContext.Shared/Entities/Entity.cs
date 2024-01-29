using Flunt.Notifications;

namespace StoreProductsContext.Shared.Entities;

public abstract class Entity : Notifiable<Notification> {
	protected Entity() {
		Id = Guid.NewGuid();
	}

	public Guid Id { get; private set; }
}