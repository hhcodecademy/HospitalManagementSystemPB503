using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Entity
{
    public class AppUser : IdentityUser
    {
        public string Location { get; set; }


    }
}
