using Microsoft.EntityFrameworkCore;
using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Repositories;
using StoreProductsContext.Domain.ValueObjects;
using StoreProductsContext.Infra.Contexts;

namespace StoreProductsContext.Infra.Repositories;

public class SupplierRepository : ISupplierRepository {
	private readonly SPContext _context;

	public SupplierRepository(SPContext context) {
		_context = context;
	}

	public void Create(Supplier supplier) {
		_context.Suppliers.Add(supplier);
		_context.SaveChanges();
	}

	public bool DocumentExists(Document document) {
		return _context.Suppliers.AsNoTracking()
			.Any(x => x.Document.Number == document.Number && x.Document.Type == document.Type);
	}

	public IEnumerable<Supplier> GetAll() {
		return _context.Suppliers.AsNoTracking();
	}
}