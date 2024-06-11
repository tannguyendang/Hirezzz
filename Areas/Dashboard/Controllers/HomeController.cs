using Microsoft.AspNetCore.Mvc;
using Hirezzz.Controllers;

namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}