using FileUploadMvcApp.Models;
using FileUploadMvcApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FileUploadMvcApp.Services
{
    public class FileService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".pdf", ".png", ".jpg" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
        
        public FileService(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        
        public async Task<(bool Success, string Message, UploadedFile? File)> UploadFileAsync(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
            {
                return (false, "Dosya seçilmedi.", null);
            }
            
            if (file.Length > MaxFileSize)
            {
                return (false, "Dosya boyutu 10MB'dan büyük olamaz.", null);
            }
            
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                return (false, "Sadece PDF, PNG, JPG dosyaları yüklenebilir.", null);
            }
            
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            
            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FilePath = $"uploads/{uniqueFileName}",
                ContentType = file.ContentType,
                FileSize = file.Length,
                UserId = userId
            };
            
            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();
            
            return (true, "Dosya başarıyla yüklendi.", uploadedFile);
        }
        
        public async Task<List<UploadedFile>> GetUserFilesAsync(int userId)
        {
            return await _context.UploadedFiles
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.UploadDate)
                .ToListAsync();
        }
        
        public async Task<(bool Success, string Message)> DeleteFileAsync(int fileId, int userId)
        {
            var file = await _context.UploadedFiles
                .FirstOrDefaultAsync(f => f.Id == fileId && f.UserId == userId);
            
            if (file == null)
            {
                return (false, "Dosya bulunamadı.");
            }
            
            var physicalPath = Path.Combine(_environment.WebRootPath, file.FilePath);
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
            
            _context.UploadedFiles.Remove(file);
            await _context.SaveChangesAsync();
            
            return (true, "Dosya başarıyla silindi.");
        }
        
        public string FormatFileSize(long bytes)
        {
            if (bytes == 0) return "0 Bytes";
            string[] sizes = { "Bytes", "KB", "MB", "GB" };
            int i = (int)Math.Floor(Math.Log(bytes) / Math.Log(1024));
            return Math.Round(bytes / Math.Pow(1024, i), 2) + " " + sizes[i];
        }
    }
} 