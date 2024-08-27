using System.Linq.Expressions;
using StoreProductsContext.Domain.Entities;

namespace StoreProductsContext.Domain.Queries;

public static class SupplierQueries {
	public static Expression<Func<Supplier, bool>> GetSupplier(int id) {
		return x => x.Id == id;
	}
}