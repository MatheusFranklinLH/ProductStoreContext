using LeMat.Domain.Entities;
using LeMat.Domain.Responses;

namespace LeMat.Domain.Repositories;

public interface IProductImageRepository {
	Task CreateAsync(Image image);
	Task DeleteAsync(Image image);
	Task<Image> GetByIdAsync(int id);
	Task<Product> GetProductByIdWithImagesAsync(int id);
	Task DeleteManyImagesAsync(List<Image> images);
	Task CreateManyImagesAsync(List<Image> images);
}