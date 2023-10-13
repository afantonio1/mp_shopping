using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MpShopping.IdentityServer.Model.Context
{
    public class MySQLDbContext : IdentityDbContext<ApplicationUser>
    {
        public MySQLDbContext(DbContextOptions<MySQLDbContext> options) 
            : base(options) { }

    }
}
