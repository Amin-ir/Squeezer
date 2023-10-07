using System.ComponentModel.DataAnnotations;

namespace Squeezer.Models
{
    public class Url
    {
        [Required]
        public int Id { get; set; }
        [Url]
        [Display(Name = "Input URL")]
        public string OriginalUrl { get; set; }
        [MinLength(5)]
        public string? ShortenedUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public string GetOriginalUrlPresentation()
        {
            return OriginalUrl.Length > 30 ? OriginalUrl.Substring(0, 30) + "..." : OriginalUrl;
        }
    }
}
