using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Hirezzz.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hirezzz.Controllers;
public class AuthController : BaseController
{
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterModel obj)
    {
        int ret = Provider.Member.Add(obj);
        if (ret > 0)
        {
            return Redirect("/");
        }
        string[] arr = { $"Username {obj.Usr} exists", "Inserted Failed" };
        //-1 username exist
        //0 Inserted failed
        ModelState.AddModelError("error", arr[ret + 1]);
        return View(obj);
    }
    //Kiem tra da dang nhap chua
    [Authorize]
    public IActionResult Index()
    {
        return View(Provider.Member.GetMember(User.FindFirstValue(ClaimTypes.NameIdentifier)));
    }
    public IActionResult Login()
    {
        LoadData();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel obj)
    {
        Member? member = Provider.Member.Login(obj);
        if (member != null)
        {
            // if (member.IsDeleted != null && member.UpdatedDate != null && member.IsDeleted.Value)
            // {
            //     ModelState.AddModelError("Error", $"Your Password change ago {DateTime.Now.Subtract(member.UpdatedDate.Value).Minutes} minutes");
            // }
            // else
            // {
            List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, member.Id),
                        new Claim(ClaimTypes.Name, member.Username),
                        new Claim(ClaimTypes.Email, member.Email),
                        new Claim(ClaimTypes.GivenName, member.Fullname),
                        new Claim(ClaimTypes.Gender, member.Gender ? "Female" : "Male")
                    };
            List<Role> list = Provider.Role.GetRolesByMember(member.Id);
            if (list != null)
            {
                foreach (var item in list)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity); //principal dung goi ham signinasync

            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                IsPersistent = obj.Rem
            });
            return Redirect("/");
            //}
        }
        ModelState.AddModelError("error", "Login Failed");
        return View(obj);
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
    [Authorize]
    public IActionResult Change()
    {
        LoadData();
        return View();
    }
    [Authorize, HttpPost]
    public IActionResult Change(ChangeModel obj)
    {
        obj.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (obj.Id != null)
        {
            int ret = Provider.Member.Change(obj);
            if (ret > 0)
            {
                return Redirect("/auth/logout");
            }
            ModelState.AddModelError("Error", "Password Failed");
            return View();
        }
        return Redirect("/auth/login");
    }
}