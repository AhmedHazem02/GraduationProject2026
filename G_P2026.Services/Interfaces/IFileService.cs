using Microsoft.AspNetCore.Http;

namespace G_P2026.Services.Interfaces
{
	public interface IFileService
	{
		Task<string> UploadFileAsync(IFormFile file, string folderName);
		Task<bool> DeleteFileAsync(string filePath);
		Task<byte[]> ConvertToByteArrayAsync(IFormFile file);
	}
}
