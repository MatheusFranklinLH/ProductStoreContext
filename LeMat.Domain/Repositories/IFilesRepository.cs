using Microsoft.AspNetCore.Http;

namespace LeMat.Domain.Repositories;

public interface IFilesRepository {
	Task<string> UploadImageAsync(IFormFile file, List<string> allowedExtensions = null, long maxFileSizeBytes = 5 * 1024 * 1024);
	Task<string> GetFileAsBase64Async(string filePath);
	Task DeleteFileAsync(string filePath);
}