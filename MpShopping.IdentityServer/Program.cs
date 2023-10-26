using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MpShopping.IdentityServer.Initializer;
using MpShopping.IdentityServer.Configuration;
using MpShopping.IdentityServer.Model.Context;
using Duende.IdentityServer.Services;
using MpShopping.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration["MySQlConnection:MySQlConnectionString"];
builder.Services.AddDbContext<MySQLDbContext>(options => options
        .UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 32))));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySQLDbContext>()
    .AddDefaultTokenProviders();

var identityServer = builder.Services.AddIdentityServer(options =>
{
    options.EmitStaticAudienceClaim = true;
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseInformationEvents = true;

}).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
  .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
  .AddInMemoryClients(IdentityConfiguration.Clients)
  .AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

identityServer.AddDeveloperSigningCredential();

var app = builder.Build();

var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

initializer?.Initialize();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
