using Hirezzz.Controllers;
using Hirezzz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class RoleController : BaseController
{
    //Get
    public IActionResult Edit(int id)
    {
        return View(Provider.Role.GetRole(id));
    }
    //Post
    [HttpPost]
    public IActionResult Edit(Role obj)
    {
        int ret = Provider.Role.Edit(obj);
        return Redirect(ret);
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        int ret = Provider.Role.Delete(id);
        return Redirect(ret);
    }
    [HttpPost]
    public IActionResult Get(int id)
    {
        return Json(Provider.Role.GetRole(id));
    }
    IActionResult Redirect(int ret)
    {
        if (ret > 0)
        {
            return Redirect("/dashboard/role");
        }
        return Redirect("/dashboard/role/error");
    }
    [HttpPost]
    public IActionResult Add(Role obj)
    {
        Random random = new Random();
        obj.Id = random.Next();
        int ret = Provider.Role.Add(obj);
        return Redirect(ret);
    }
    public IActionResult Index()
    {
        return View(Provider.Role.GetRoles());
    }
}
