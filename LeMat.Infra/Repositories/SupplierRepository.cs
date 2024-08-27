using LeMat.Domain.Entities;
using LeMat.Domain.Repositories;
using LeMat.Domain.ValueObjects;
using LeMat.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LeMat.Infra.Repositories;

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