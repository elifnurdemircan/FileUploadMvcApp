using System.ComponentModel.DataAnnotations;

namespace FileUploadMvcApp.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public virtual ICollection<UploadedFile> Files { get; set; } = new List<UploadedFile>();
    }
} 