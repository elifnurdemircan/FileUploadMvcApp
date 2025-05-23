namespace FileUploadMvcApp.Models
{
    public class FileUploadViewModel
    {
        public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
} 