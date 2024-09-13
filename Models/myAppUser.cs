using Microsoft.AspNetCore.Identity;

namespace Regstration.Models
{
    public class myAppUser:IdentityUser


    {
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

    }
}
