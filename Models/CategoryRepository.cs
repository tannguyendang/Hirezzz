using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Hirezzz.Models;
public class CategoryRepository : BaseRepository
{
    public CategoryRepository(MusicContext context) : base(context)
    {
    }
    public Category? GetCategoryById(byte id)
    {
        //return context.Categories.SingleOrDefault(p => p.Id == id);
        return context.Categories.Find(id);
    }
    public List<Category> GetParents()
    {
        return context.Categories.Where(p => p.ParentId == null).Include(p => p.Children).ToList();
    }
    public List<Category> GetCategoriesByParent(byte id)
    {
        //Miss
        return context.Categories.Include(p => p.Products.Take(6)).Where(p => p.ParentId == id).ToList();
    }
    public List<Category> GetCategories()
    {
        return context.Categories.ToList();
    }
    public int Add(Category obj)
    {
        return context.Database.ExecuteSqlRaw("EXEC AddCategory @CategoryName, @ParentId", new SqlParameter[]{
            new SqlParameter("@CategoryName", obj.Name),
            new SqlParameter("@ParentId", obj.ParentId)
        });
    }
    public int Delete(byte id)
    {
        context.Categories.Remove(new Category { Id = id });
        return context.SaveChanges();
    }
    public int Edit(Category obj)
    {
        context.Categories.Update(obj);
        return context.SaveChanges();
    }
}