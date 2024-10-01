using LeMat.Domain.Entities;

namespace LeMat.Domain.Responses;

public class ProductResponse {
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal SuggestedSellPrice { get; set; }
	public decimal MaximumDiscountPercentage { get; set; }
	public string SupplierName { get; set; }
	public int? SupplierId { get; set; }

}

public static partial class ResponseExtensions {
	public static IQueryable<ProductResponse> MapToProductResponse(this IQueryable<Product> products) {
		return products.Select(x => new ProductResponse() {
			Id = x.Id,
			Name = x.Name,
			SuggestedSellPrice = x.SuggestedSellPrice,
			MaximumDiscountPercentage = x.MaximumDiscountPercentage,
			SupplierName = x.Supplier != null ? x.Supplier.Name : "",
			SupplierId = x.SupplierId
		});
	}

	public static ProductResponse MapToProductResponse(this Product products) {
		return new ProductResponse() {
			Id = products.Id,
			Name = products.Name,
			SuggestedSellPrice = products.SuggestedSellPrice,
			MaximumDiscountPercentage = products.MaximumDiscountPercentage,
			SupplierName = products.Supplier != null ? products.Supplier.Name : "",
			SupplierId = products.SupplierId
		};
	}
}