using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Models;
public class ProductRepository : BaseRepository
{
    public ProductRepository(MusicContext context) : base(context)
    {
    }
    public List<Product> GetProducts()
    {
        return context.Products.ToList();
    }
    // public List<Country> GetCountry(byte CountryId)
    // {
    //     return context.Countries.FromSqlRaw("EXEC GetCountry @CountryId", new SqlParameter("@CountryId", CountryId)).ToList();
    // }
    public Product? GetProductById(int id)
    {
        return context.Products.Find(id);
    }
    public int CountByCategory(int id)
    {
        return context.Products.Where(p => p.CategoryId == id).Count();
    }
    public List<Product> GetProductsByCategory(byte id, int page, int size = 6)
    {
        return context.Products.Where(p => p.CategoryId == id).Skip((page - 1) * size).Take(size).ToList();
    }
    public List<Product> GetProductsNewest()
    {
        return context.Products.Include(p => p.Category).Where(p => p.Category.ParentId == 1).OrderByDescending(p => p.Id).ToList();
    }
    // public List<Product> GetSongs(int id)
    // {
    //     return context.Products.FromSqlRaw("EXEC GetSongs @ProductId", new SqlParameter("@ProductId", id)).ToList();
    // }
    public int Add(Product obj)
    {
        context.Products.Add(obj);
        return context.SaveChanges();
    }
    public int Delete(int id)
    {
        context.Products.Remove(new Product { Id = id });
        return context.SaveChanges();
    }
    // public int Edit(Product obj)
    // {
    //     context.Products.Update(obj);
    //     return context.SaveChanges();
    // }
}