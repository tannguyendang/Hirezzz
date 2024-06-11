using Microsoft.AspNetCore.Mvc;
using Hirezzz.Models;

namespace Hirezzz.Controllers;
public abstract class BaseController : Controller
{
    SiteProvider provider = null!;
    protected SiteProvider Provider => provider ??= new SiteProvider(HttpContext.RequestServices.GetRequiredService<MusicContext>());
    protected void LoadData()
    {
        ViewBag.Categories = Provider.Category.GetParents();
        List<Banner> list = Provider.Banner.GetBanners();
        ViewBag.slides = list.Where(p => p.Type == 0);
        ViewBag.advertisesL = list.Where(p => p.Type == 1);
        ViewBag.advertisesR = list.Where(p => p.Type == 2);
    }
}