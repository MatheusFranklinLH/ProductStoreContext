using LeMat.Domain.Entities;
using LeMat.Domain.Repositories;
using LeMat.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LeMat.Infra.Repositories;

public class ProductImageRepository : IProductImageRepository {
	private readonly SPContext _context;

	public ProductImageRepository(SPContext context) {
		_context = context;
	}

	public async Task CreateAsync(Image image) {
		await _context.Images.AddAsync(image);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Image image) {
		_context.Images.Remove(image);
		await _context.SaveChangesAsync();
	}

	public async Task<Image> GetByIdAsync(int id) {
		return await _context.Images.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<Product> GetProductByIdWithImagesAsync(int id) {
		return await _context.Products.AsNoTracking()
			.Include(x => x.Images)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task DeleteManyImagesAsync(List<Image> images) {
		_context.Images.RemoveRange(images.ToArray());
		await _context.SaveChangesAsync();
	}

	public async Task CreateManyImagesAsync(List<Image> images) {
		await _context.Images.AddRangeAsync(images);
		await _context.SaveChangesAsync();
	}
}