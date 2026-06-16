using System.ComponentModel.DataAnnotations;

namespace APIAnimeHub.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
