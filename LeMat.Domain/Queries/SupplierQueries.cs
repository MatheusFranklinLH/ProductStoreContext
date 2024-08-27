using System.Linq.Expressions;
using LeMat.Domain.Entities;

namespace LeMat.Domain.Queries;

public static class SupplierQueries {
	public static Expression<Func<Supplier, bool>> GetSupplier(int id) {
		return x => x.Id == id;
	}
}