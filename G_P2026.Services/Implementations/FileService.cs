using G_P2026.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace G_P2026.Services.Implementations
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public FileService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<string> UploadFileAsync(IFormFile file, string folderName)
		{
			if (file == null || file.Length == 0)
				throw new ArgumentException("File is empty or null");

			// Create unique filename
			var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

			// Create folder path (wwwroot/uploads/folderName)
			var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folderName);

			// Create directory if it doesn't exist
			if (!Directory.Exists(uploadsFolder))
			{
				Directory.CreateDirectory(uploadsFolder);
			}

			// Full file path
			var filePath = Path.Combine(uploadsFolder, fileName);

			// Save file
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			// Return relative path for database storage
			return $"/uploads/{folderName}/{fileName}";
		}

		public async Task<bool> DeleteFileAsync(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				return false;

			try
			{
				var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

				if (File.Exists(fullPath))
				{
					await Task.Run(() => File.Delete(fullPath));
					return true;
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
		{
			if (file == null || file.Length == 0)
				throw new ArgumentException("File is empty or null");

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
