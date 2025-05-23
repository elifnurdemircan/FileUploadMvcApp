using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FileUploadMvcApp.Services;
using FileUploadMvcApp.Models;

namespace FileUploadMvcApp.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
    {
        private readonly FileService _fileService;
        
        public FileUploadController(FileService fileService)
        {
            _fileService = fileService;
        }
        
        public async Task<IActionResult> Index(string? message = null, bool isSuccess = false)
        {
            var userId = GetCurrentUserId();
            var files = await _fileService.GetUserFilesAsync(userId);
            
            var viewModel = new FileUploadViewModel
            {
                Files = files,
                Message = message,
                IsSuccess = isSuccess
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var userId = GetCurrentUserId();
            
            var result = await _fileService.UploadFileAsync(file, userId);
            
            return RedirectToAction("Index", new { message = result.Message, isSuccess = result.Success });
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _fileService.DeleteFileAsync(id, userId);
            
            return RedirectToAction("Index", new { message = result.Message, isSuccess = result.Success });
        }
        
        public IActionResult Download(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = GetContentType(filePath);
            
            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }
        
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim!.Value);
        }
        
        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }
    }
} 