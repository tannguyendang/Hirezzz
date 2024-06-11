using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Models;
public class LibraryRepository : BaseRepository
{
    public LibraryRepository(MusicContext context) : base(context)
    {
    }
    public int Add(Library obj)
    {
        return context.Database.ExecuteSqlRaw("EXEC AddLibrary @LibId, @MemberId, @ProductId", new SqlParameter[]{
            new SqlParameter("@LibId", obj.LibId),
            new SqlParameter("@MemberId", obj.MemberId),
            new SqlParameter("@ProductId", obj.ProductId),
        });
    }
    public List<Library> GetLibraries(string uId)
    {
        return context.Libraries.FromSqlRaw("EXEC GetLibraries @MemberId", new SqlParameter("@MemberId", uId)).ToList();
    }
    public Product? GetSong(byte id)
    {
        return context.Products.Find(id);
    }
    public int Delete(string id)
    {
        return context.Database.ExecuteSqlRaw("EXEC DeleteLibrary @LibId", new SqlParameter[]{
            new SqlParameter("@LibId", id)
        });
    }
}