using System.ComponentModel.DataAnnotations;

namespace DeanHLibrarySite.Models
{
    public class BookTable
    {
        public int Id { get; set; }
        public enum BookType
        {
            Book,
            DVD,
            CD
        }

        public BookType Type { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; } = string.Empty;
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Genre { get; set; } = string.Empty;

        [Display(Name = "Publication Year"), DataType(DataType.Date)]
        public DateTime PublicationYear { get; set; }
        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
