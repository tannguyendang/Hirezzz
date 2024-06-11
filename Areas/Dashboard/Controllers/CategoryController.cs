using Microsoft.AspNetCore.Mvc;
using Hirezzz.Controllers;
using Hirezzz.Models;

namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class CategoryController : BaseController
{
    public IActionResult Index()
    {
        return View(Provider.Category.GetCategories());
    }
    IActionResult Redirect(int ret)
    {
        if (ret > 0)
        {
            return Redirect("/dashboard/category");
        }
        return Redirect("/dashboard/category/error");
    }
    //Get
    public IActionResult Edit(byte id)
    {
        return View(Provider.Category.GetCategoryById(id));
    }
    //Post
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        int ret = Provider.Category.Edit(obj);
        return Redirect(ret);
    }
    [HttpPost]
    public IActionResult Add(Category obj)
    {
        int ret = Provider.Category.Add(obj);
        return Redirect(ret);
    }
    public IActionResult Delete(byte id)
    {
        int ret = Provider.Category.Delete(id);
        return Redirect(ret);
    }
}