using LeMat.Domain.Entities;
using LeMat.Domain.ValueObjects;

namespace LeMat.Domain.Responses;

public record SupplierResponse(int Id, string Name, string CompanyReason, Telephone Telephone, Email Email, Address Address, Document Document);

public static partial class ResponseExtensions {
	public static IQueryable<SupplierResponse> MapToSupplierResponse(this IQueryable<Supplier> suppliers) {
		return suppliers.Select(x => new SupplierResponse(x.Id, x.Name, x.CompanyReason, x.Telephone, x.Email, x.Address, x.Document));
	}
}