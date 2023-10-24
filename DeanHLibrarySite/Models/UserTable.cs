using System.ComponentModel.DataAnnotations;

namespace DeanHLibrarySite.Models
{
    public class UserTable
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "User Name")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Has Administrator Access")]
        [Required]
        public bool HasAdministrator { get; set; } = false;
    }
}
