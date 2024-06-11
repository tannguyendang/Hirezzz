using System.Data.Entity.Infrastructure;
using System.Security.Claims;
using Hirezzz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Controllers;
public class LibraryController : BaseController
{
    public IActionResult Index()
    {
        LoadData();
        return View();
    }
    [HttpPost]
    public IActionResult GetSong()
    {
        string? uId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uId != null)
        {
            return Json(Provider.Library.GetLibraries(uId));
        }
        return Redirect("/");
    }
    public IActionResult Add(Library obj)
    {
        obj.LibId = Guid.NewGuid().ToString().Replace("-", string.Empty);
        string? uId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uId != null)
        {
            //int productId = id.ProductId;
            obj.MemberId = uId;
            int ret = Provider.Library.Add(obj);
            if (ret > 0)
            {
                return Redirect("/library");
            }
            ModelState.AddModelError("Error", "Checkout failed");
        }
        return Redirect("/");
    }
    public IActionResult Delete(string id)
    {
        int ret = Provider.Library.Delete(id);
        if (ret > 0)
        {
            return Redirect("/library");
        }
        return Redirect("/library/error");
    }
}