using Microsoft.AspNetCore.Identity;

namespace MpShopping.IdentityServer.Model.Context
{
    public class ApplicationUser : IdentityUser
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
    }
}
