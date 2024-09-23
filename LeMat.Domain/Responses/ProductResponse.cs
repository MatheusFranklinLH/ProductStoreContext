using LeMat.Domain.Entities;

namespace LeMat.Domain.Responses;

public record ProductResponse(
	int Id,
	string Name,
	decimal SuggestedSellPrice,
	decimal MaximumDiscountPercentage,
	string SupplierName,
	int? SupplierId
);

public static partial class ResponseExtensions {
	public static IQueryable<ProductResponse> MapToProductResponse(this IQueryable<Product> products) {
		return products.Select(x => new ProductResponse(
			x.Id,
			x.Name,
			x.SuggestedSellPrice,
			x.MaximumDiscountPercentage,
			x.Supplier.Name,
			x.SupplierId));
	}
}