using System.ComponentModel.DataAnnotations;

namespace Squeezer.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
