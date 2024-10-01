using LeMat.Domain.Entities;

namespace LeMat.Domain.DTOs;

public record NameId(
	int Id,
	string Name
);

public static partial class DTOsExtensions {
	public static IQueryable<NameId> MapToNameId(this IQueryable<Product> products) {
		return products.Select(x => new NameId(
			x.Id,
			x.Name));
	}

	public static IQueryable<NameId> MapToNameId(this IQueryable<Supplier> suppliers) {
		return suppliers.Select(x => new NameId(
			x.Id,
			x.Name));
	}
}