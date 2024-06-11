using Hirezzz.Controllers;
using Hirezzz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hirezzz.Area.Dashboard.Controllers;
[Area("Dashboard")]
public class MemberController : BaseController
{
    public IActionResult Roles(string id)
    {
        ViewBag.roles = Provider.Member.GetRoleByMember(id);
        return View(Provider.Member.GetMember(id));
    }
    [HttpPost]
    public IActionResult AddRole(MemberInRole obj)
    {
        return Json(Provider.Member.Add(obj));
    }
    public IActionResult Index()
    {
        return View(Provider.Member.GetMembers());
    }
    IActionResult Redirect(int ret)
    {
        if (ret > 0)
        {
            return Redirect("/dashboard/member");
        }
        return Redirect("/dashboard/member/error");
    }
    //Get
    public IActionResult Edit(string id)
    {
        return View(Provider.Member.GetMember(id));
    }
    //Post
    [HttpPost]
    public IActionResult Edit(Member obj)
    {
        int ret = Provider.Member.Edit(obj);
        return Redirect(ret);
    }
    public IActionResult Delete(string id)
    {
        int ret = Provider.Member.Delete(id);
        return Redirect(ret);
    }
}