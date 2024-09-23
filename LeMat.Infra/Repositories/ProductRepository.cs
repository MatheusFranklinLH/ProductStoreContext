using LeMat.Domain.Entities;
using LeMat.Domain.Repositories;
using LeMat.Domain.Responses;
using LeMat.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LeMat.Infra.Repositories;

public class ProductRepository : IProductRepository {
	private readonly SPContext _context;

	public ProductRepository(SPContext context) {
		_context = context;
	}

	public async Task CreateAsync(Product product) {
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Product product) {
		_context.Products.Update(product);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Product product) {
		_context.Products.Remove(product);
		await _context.SaveChangesAsync();
	}

	public async Task<List<ProductResponse>> GetAllAsync() {
		return await _context.Products.AsNoTracking().MapToProductResponse().ToListAsync();
	}

	public async Task<Product> GetByIdAsync(int id) {
		return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
	}
}