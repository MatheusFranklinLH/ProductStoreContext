using System.Text.Json.Serialization;
using Flunt.Notifications;

namespace StoreProductsContext.Shared.ValueObjects;

public abstract class ValueObject : Notifiable<Notification> {
	[JsonIgnore]
	public new IReadOnlyCollection<Notification> Notifications { get; }
	[JsonIgnore]
	public new bool IsValid { get; }
}