using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hirezzz.Models;
public class BannerRepository : BaseRepository
{
    public BannerRepository(MusicContext context) : base(context)
    {
    }
    public List<Banner> GetBanners()
    {
        return context.Banners.ToList();
    }
    public Banner? GetBanner(byte id)
    {
        return context.Banners.Find(id);
    }
    public List<Banner> GetBannersByType(byte type)
    {
        return context.Banners.Where(p => p.Type == type).ToList();
    }
    public int Add(Banner obj)
    {
        return context.Database.ExecuteSqlRaw("EXEC AddBanner @BannerName, @ImageUrl, @BannerType", new SqlParameter[]{
            new SqlParameter("@BannerName", obj.Name),
            new SqlParameter("@ImageUrl", obj.ImageUrl),
            new SqlParameter("@BannerType", obj.Type)
        });
    }
    // public int Add(Banner obj)
    // {
    //     context.Banners.Add(obj);
    //     return context.SaveChanges();
    // }
    public int Delete(byte id)
    {
        context.Banners.Remove(new Banner { Id = id });
        return context.SaveChanges();
    }
    public int Edit(Banner obj)
    {
        Banner banner = context.Banners.Find(obj.Id);
        banner.ImageUrl = obj.ImageUrl;
        banner.Name = obj.Name;
        banner.Type = obj.Type;
        context.Update(banner);
        return context.SaveChanges();
    }
    public int Update(Banner obj)
    {
        return context.Database.ExecuteSqlRaw("EXEC EditBanner @BannerId, @BannerName, @ImageUrl, @BannerType", new SqlParameter[]{
            new SqlParameter("@BannerId", obj.Id),
            new SqlParameter("@BannerName", obj.Name),
            new SqlParameter("@ImageUrl", obj.ImageUrl),
            new SqlParameter("@BannerType", obj.Type)
        });
    }
}