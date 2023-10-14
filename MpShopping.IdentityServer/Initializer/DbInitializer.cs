using IdentityModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MpShopping.IdentityServer.Configuration;
using MpShopping.IdentityServer.Model.Context;

namespace MpShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLDbContext _context;
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitializer(MySQLDbContext context,
                             RoleManager<IdentityRole> role,
                             UserManager<ApplicationUser> userManager)
        {
            _role = role;
            _context = context;
            _userManager = userManager;
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            var admin = new ApplicationUser()
            {
                UserName = "alessandro-admin",
                Email = "alessandroadmin@micropont.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (31) 98412-0000",
                FirstName = "Alessandro",
                LastName = "Admin"
            };

            var user = _userManager.CreateAsync(admin, "Mpshopping$123").GetAwaiter().GetResult();
            var userRoles = _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "alessandro-client",
                Email = "alessandroclient@micropont.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (31) 98412-0000",
                FirstName = "Alessandro",
                LastName = "client"
            };
            _userManager.CreateAsync(client, "Mpshopping$123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;

        }
    }
}
