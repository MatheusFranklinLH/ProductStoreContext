using System.Text.RegularExpressions;
using LeMat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace LeMat.Infra.Repositories;
public class FilesRepository : IFilesRepository {
	public FilesRepository() {
		if (!Directory.Exists(_uploadDirectory))
			Directory.CreateDirectory(_uploadDirectory);
	}
	private static readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "static");

	private string GetSafeFilePath(string fileName) {
		var fullFilePath = Path.Combine(_uploadDirectory, fileName);
		fullFilePath = Path.GetFullPath(fullFilePath);

		var fullUploadDirectory = Path.GetFullPath(_uploadDirectory);

		if (!fullFilePath.StartsWith(fullUploadDirectory, StringComparison.OrdinalIgnoreCase))
			throw new UnauthorizedAccessException("Invalid file path");

		return fullFilePath;
	}

	public async Task<string> UploadImageAsync(IFormFile file, List<string> allowedExtensions = null, long maxFileSizeBytes = 5 * 1024 * 1024) {
		if (file == null || file.Length == 0)
			throw new ArgumentException("Arquivo inválido");

		if (maxFileSizeBytes > 0 && file.Length > maxFileSizeBytes)
			throw new ArgumentException($"O arquivo excede o tamanho máximo permitido de {maxFileSizeBytes / 1024 / 1024}MB");

		var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

		if (!Regex.IsMatch(fileExtension, @"^\.[a-z0-9]+$"))
			throw new ArgumentException("Extensão do arquivo inválida");

		if (allowedExtensions != null && allowedExtensions.Any() && !allowedExtensions.Contains(fileExtension))
			throw new ArgumentException("Extensão de arquivo não permitida");

		var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
		var filePath = GetSafeFilePath(uniqueFileName);

		using (var stream = new MemoryStream()) {
			await file.CopyToAsync(stream);
			stream.Position = 0;

			try {
				using (var image = await Image.LoadAsync(stream)) {
					stream.Position = 0;
					using (var fileStream = new FileStream(filePath, FileMode.Create)) {
						await stream.CopyToAsync(fileStream);
					}
				}
			}
			catch (UnknownImageFormatException) {
				throw new ArgumentException("O arquivo não é uma imagem válida");
			}
		}

		return uniqueFileName;
	}


	public async Task<string> GetFileAsBase64Async(string fileName) {
		var filePath = GetSafeFilePath(fileName);
		if (!File.Exists(filePath))
			throw new FileNotFoundException("Arquivo não encontrado");

		byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
		return Convert.ToBase64String(fileBytes);
	}

	public async Task DeleteFileAsync(string fileName) {
		var filePath = GetSafeFilePath(fileName);
		if (!File.Exists(filePath))
			throw new FileNotFoundException("Arquivo não encontrado");
		await Task.Run(() => File.Delete(filePath));
	}
}