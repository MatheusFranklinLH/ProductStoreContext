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

	public async Task CreateAsync(Supplier supplier) {
		await _context.Suppliers.AddAsync(supplier);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> DocumentExistsAsync(Document document) {
		return await _context.Suppliers.AsNoTracking()
			.AnyAsync(x => x.Document.Number == document.Number && x.Document.Type == document.Type);
	}

	public async Task<List<Supplier>> GetAllAsync() {
		return await _context.Suppliers.AsNoTracking().ToListAsync();
	}
}