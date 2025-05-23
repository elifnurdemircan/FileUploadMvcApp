using System.ComponentModel.DataAnnotations;

namespace FileUploadMvcApp.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        
        [Required]
        public string FileName { get; set; } = string.Empty;
        
        [Required]
        public string FilePath { get; set; } = string.Empty;
        
        [Required]
        public string ContentType { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        // Helper property to get just the filename from FilePath
        public string FileNameOnly => System.IO.Path.GetFileName(FilePath);
    }
} 