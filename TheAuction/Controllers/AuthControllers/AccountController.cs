using TheAuction.Models;
using System.Collections.Generic;
using TheAuction.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CodeFirst;

namespace TheAuction.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult ChooseRole()
        {
            return View();
        }

        //[AllowAnonymous]
        //public void LoginAsCustomer()
        //{
        //    string RoleName = "Customers";
        //    string returnUrl = "/Home/Index";
        //    Login(returnUrl, RoleName);
        //}

        [AllowAnonymous]
        public ActionResult LoginAsCustomer()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "В доступе отказано" });
            }
            ViewBag.RoleName = "Customers";
            //ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        public ActionResult LoginAsSeller()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "В доступе отказано" });
            }
            ViewBag.RoleName = "Sellers";
           //ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel details, string RoleName)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);

            if (user != null)
            {
                if (RoleName == "Customers")
                {
                    AppRole roleCustomers = RoleManager.FindByName(RoleName);
                    AppRole roleSellers = RoleManager.FindByName("Sellers");
                    if ((roleCustomers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                    {
                        if ((roleSellers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                        {
                            UserManager.RemoveFromRole(user.Id, "Sellers");
                        }
                    }
                    else
                    {
                        UserManager.AddToRole(user.Id, RoleName);
                        if ((roleSellers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                        {
                            UserManager.RemoveFromRole(user.Id, "Sellers");
                        }
                    }
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                    return RedirectToAction("IndexLots", "Auction");
                }
                if (RoleName == "Sellers")
                {
                    AppRole roleCustomers = RoleManager.FindByName(RoleName);
                    AppRole roleSellers = RoleManager.FindByName("Customers");
                    if ((roleCustomers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                    {
                        if ((roleSellers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                        {
                            UserManager.RemoveFromRole(user.Id, "Customers");
                        }
                    }
                    else
                    {
                        UserManager.AddToRole(user.Id, RoleName);
                        if ((roleSellers.Users.FirstOrDefault(i => i.UserId == user.Id)) != null)
                        {
                            UserManager.RemoveFromRole(user.Id, "Customers");
                        }
                    }
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                    return RedirectToAction("IndexLots", "Auction");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка авторизации, такой роли не существует.");
                    return View("LoginAsCustomer", details);
                }
            }
            else
            {
                if (RoleName == "Customers")
                {
                    ViewBag.RoleName = "Customers";
                    ModelState.AddModelError("", "Некорректное имя или пароль.");
                    return View("LoginAsCustomer", details);
                }
                if (RoleName == "Sellers")
                {
                    ViewBag.RoleName = "Sellers";
                    ModelState.AddModelError("", "Некорректное имя или пароль.");
                    return View("LoginAsSeller", details);
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка авторизации, такой роли не существует.");
                    return View("LoginAsCustomer", details);
                }
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("IndexLots", "Auction");
        }  

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}