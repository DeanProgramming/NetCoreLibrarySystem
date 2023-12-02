using System.ComponentModel.DataAnnotations;

namespace DeanHLibrarySite.Models
{
    public class BookReservations
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public string? UserID { get; set; }
        public bool Booked { get; set; }
        [Display(Name = "Return Date"), DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; } 
    }
}
