using LeMat.Domain.Entities;
using LeMat.Domain.Responses;

namespace LeMat.Domain.Repositories;

public interface IProductRepository {
	Task CreateAsync(Product product);
	Task UpdateAsync(Product product);
	Task DeleteAsync(Product product);
	Task<Product> GetByIdAsync(int id);
	Task<Product> GetByIdWithImagesAsync(int id);
	Task<List<ProductResponse>> GetAllAsync();
	Task DeleteManyImagesAsync(List<Image> images);
	Task InsertManyImagesAsync(List<Image> images);
}