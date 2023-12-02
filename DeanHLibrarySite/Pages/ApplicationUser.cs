
using Microsoft.AspNetCore.Identity;

namespace DeanHLibrarySite.Pages
{

    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public bool IsAdmin { get; set; }
    }
}
