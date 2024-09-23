using Microsoft.AspNetCore.Http;

namespace LeMat.Shared.Utils;
public static class FileUtils {
	private static readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "static");

	public static async Task<string> UploadFileAsync(IFormFile file, List<string> allowedExtensions = null, long maxFileSizeBytes = 5 * 1024 * 1024) {
		if (file == null || file.Length == 0)
			throw new ArgumentException("Arquivo inválido");

		if (maxFileSizeBytes > 0 && file.Length > maxFileSizeBytes)
			throw new ArgumentException($"O arquivo excede o tamanho máximo permitido de {maxFileSizeBytes / 1024 / 1024}MB");

		var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

		if (allowedExtensions != null && allowedExtensions.Any() && !allowedExtensions.Contains(fileExtension))
			throw new ArgumentException("Extensão de arquivo não permitida");

		if (!Directory.Exists(_uploadDirectory))
			Directory.CreateDirectory(_uploadDirectory);

		var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
		var filePath = Path.Combine(_uploadDirectory, uniqueFileName);

		using (var stream = new FileStream(filePath, FileMode.Create)) {
			await file.CopyToAsync(stream);
		}

		return filePath;
	}

	public static byte[] GetFile(string filePath) {
		if (!File.Exists(filePath))
			throw new FileNotFoundException("Arquivo não encontrado");

		return File.ReadAllBytes(filePath);
	}

	public static void DeleteFile(string filePath) {
		if (File.Exists(filePath))
			File.Delete(filePath);
		else
			throw new FileNotFoundException("Arquivo não encontrado");
	}
}