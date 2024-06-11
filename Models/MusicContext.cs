using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Models;
public class MusicContext : DbContext
{
    public MusicContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Banner> Banners { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<MemberPassword> MemberPasswords { get; set; } = null!;
    public DbSet<MemberStringPassword> MemberStringPasswords { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<ProductType> ProductTypes { get; set; } = null!;
    public DbSet<Library> Libraries { get; set; } = null!;
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleChecked> RoleCheckeds { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Library>().HasKey(p => new { p.LibId, p.MemberId });
        modelBuilder.Entity<MemberPassword>().HasKey(p => new { p.MemberId, p.Password });
        modelBuilder.Entity<MemberStringPassword>().HasKey(p => new { p.MemberId, p.Password });
    }
}