using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hirezzz.Models;

namespace Hirezzz.Controllers;
public class HomeController : BaseController
{
    int size = 6;
    public IActionResult Index()
    {
        ViewData["title"] = "Music Online";
        LoadData();
        return View(new dynamic[]{
            new {Title = "Mới Phát Hành", Products = Provider.Product.GetProductsNewest()}
        });
    }
    public IActionResult Details(int id)
    {
        LoadData();
        Product? obj = Provider.Product.GetProductById(id);
        if (obj != null)
        {
            ViewData["title"] = obj.Name;
            return View(obj);
        }
        return Redirect("/");
    }
    [HttpPost("/home/products/{id}/{page}")]
    public IActionResult Products(byte id, int page)
    {
        return Json(Provider.Product.GetProductsByCategory(id, page, size));
    }
    public IActionResult Parent(byte id)
    {
        LoadData();
        Category? parent = Provider.Category.GetCategoryById(id);
        if (parent != null)
        {
            ViewData["title"] = parent.Name;
            parent.Children = Provider.Category.GetCategoriesByParent(id);
        }
        return View(parent);
    }

    public IActionResult Children(byte id)
    {
        //Tái sử dụng của hàm
        LoadData();
        Category? category = Provider.Category.GetCategoryById(id);
        if (category != null)
        {
            ViewData["title"] = category.Name;
            category.Products = Provider.Product.GetProductsByCategory(id, 1, size);
        }
        //ViewBag.totals = Provider.Product.CountByCategory(id);
        ViewBag.pages = (Provider.Product.CountByCategory(id) - 1) / size + 1;
        return View(category);
    }
    // [HttpPost]
    // public IActionResult GetProduct(int id)
    // {
    //     LoadData();
    //     Product? obj = Provider.Product.GetProductById(id);
    //     if (obj != null)
    //     {
    //         ViewData["title"] = obj.Name;
    //         return Json(obj);
    //     }
    //     return Redirect("/");
    // }
    [HttpGet("{controller}/{action}/{id}")]
    public IActionResult Down(int id)
    {
        Product? obj = Provider.Product.GetProductById(id);
        string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "musics", obj.ProductUrl);
        Stream stream = new FileStream(root, FileMode.Open);
        return File(stream, "application/octet-stream", obj.ProductUrl);
    }
    public IActionResult Show(int id)
    {
        string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "musics");
        Product? obj = Provider.Product.GetProductById(id);
        return PhysicalFile(Path.Combine(root, obj.ProductUrl), "application/octet-stream");
    }
}
