using Hirezzz.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MusicContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("Hirezzz")));
builder.Services.AddScoped<SiteProvider>();
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(p =>
{
    p.LoginPath = "/auth/login";
    p.LogoutPath = "/auth/logout";
    p.ExpireTimeSpan = TimeSpan.FromDays(30);
});
builder.Services.AddAuthorization(p =>
{
    p.AddPolicy("Manager", q => q.RequireClaim(ClaimTypes.Role, "Admin", "user", "vip"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
//Use Areas
app.MapControllerRoute(name: "Dashboard", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.UseRouting();
app.UseAuthorization();
app.Run();
