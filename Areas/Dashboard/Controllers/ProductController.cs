using Microsoft.AspNetCore.Mvc;
using Hirezzz.Controllers;
using Hirezzz.Models;

namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class ProductController : BaseController
{
    public IActionResult Index()
    {
        return View(Provider.Product.GetProducts());
    }
    IActionResult Redirect(int ret)
    {
        if (ret > 0)
        {
            return Redirect("/dashboard/product");
        }
        return Redirect("/dashboard/product/error");
    }
    // //Get
    // public IActionResult Edit(byte id)
    // {
    //     return View(Provider.Product.GetProductById(id));
    // }
    // //Post
    // [HttpPost]
    // public IActionResult Edit(Product obj)
    // {
    //     int ret = Provider.Product.Edit(obj);
    //     return Redirect(ret);
    // }
    // [HttpPost]
    // public IActionResult Add(Product obj)
    // {
    //     int ret = Provider.Product.Add(obj);
    //     return Redirect(ret);
    // }
    public IActionResult Delete(byte id)
    {
        int ret = Provider.Product.Delete(id);
        return Redirect(ret);
    }
    [HttpPost]
    public IActionResult Add(Product obj, IFormFile f, IFormFile m)
    {
        System.Console.WriteLine("***********************************************************************************");
        if (f != null && m != null)
        {
            //ImageUr
            //Toi dau co viet kieu nay?
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            using (Stream stream = new FileStream(Path.Combine(root, "products", f.FileName), FileMode.Create))
            {
                f.CopyTo(stream);
            }
            using (Stream stream = new FileStream(Path.Combine(root, "musics", m.FileName), FileMode.Create))
            {
                m.CopyTo(stream);
            }
            obj.ProductUrl = m.FileName;
            obj.ImageUrl = f.FileName;
            int ret = Provider.Product.Add(obj);
            if (ret > 0)
            {
                return Redirect("/dashboard/product");
            }
        }
        return Redirect("/product/error");
    }
}