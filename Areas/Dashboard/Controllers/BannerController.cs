using Microsoft.AspNetCore.Mvc;
using Hirezzz.Controllers;
using Hirezzz.Models;


namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class BannerController : BaseController
{
    public IActionResult Index()
    {
        return View(Provider.Banner.GetBanners());
    }
    IActionResult Redirect(int ret)
    {
        if (ret > 0)
        {
            return Redirect("/dashboard/banner");
        }
        return Redirect("/dashboard/banner/error");
    }
    public IActionResult Delete(byte id)
    {
        int ret = Provider.Banner.Delete(id);
        return Redirect(ret);
    }
    [HttpPost]
    public IActionResult Add(Banner obj, IFormFile f)
    {
        if (f != null)
        {
            //ImageUrl
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            using (Stream stream = new FileStream(Path.Combine(root, "slides", f.FileName), FileMode.Create))
            {
                f.CopyTo(stream);
            }
            obj.ImageUrl = f.FileName;
            int ret = Provider.Banner.Add(obj);
            if (ret > 0)
            {
                return Redirect("/dashboard/banner");
            }
        }
        return Redirect("/dashboard/banner/error");
    }
    public IActionResult Edit(byte id)
    {
        return View(Provider.Banner.GetBanner(id));
    }
    [HttpPost]
    public IActionResult Edit(Banner obj, IFormFile f)
    {
        if (f != null)
        {
            //ImageUrl
            string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //Xoa file cu
            System.IO.File.Delete(Path.Combine(root, obj.ImageUrl));
            using (Stream stream = new FileStream(Path.Combine(root, "slides", f.FileName), FileMode.Create))
            {
                f.CopyTo(stream);
            }
            obj.ImageUrl = f.FileName;
            int ret = Provider.Banner.Update(obj);
            if (ret > 0)
            {
                return Redirect("/dashboard/banner");
            }
        }
        return Redirect("/dashboard/banner/error");
    }
}