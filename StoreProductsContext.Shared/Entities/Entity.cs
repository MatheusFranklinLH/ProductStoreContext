using Flunt.Notifications;

namespace StoreProductsContext.Shared.Entities;

public abstract class Entity : Notifiable<Notification>, IEquatable<Entity> {
	public Entity() {
		Id = 0;
	}
	public int Id { get; set; }

	public bool Equals(Entity other) {
		return Id == other.Id;
	}

	public override bool Equals(object obj) {
		return Equals(obj as Entity);
	}

	public override int GetHashCode() {
		return HashCode.Combine(Id);
	}

	public static bool operator ==(Entity left, Entity right) {
		if (ReferenceEquals(left, right))
			return true;

		if (left is null || right is null)
			return false;

		return left.Equals(right);
	}

	public static bool operator !=(Entity left, Entity right) {
		return !(left == right);
	}
}