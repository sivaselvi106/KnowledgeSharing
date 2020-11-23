using KnowledgeSharing.ServiceLayer;
using KnowledgeSharing.ViewModels;
using System.Web.Mvc;
using System;
using KnowledgeSharing.CustomFilters;
namespace KnowledgeSharing.Controllers
{
    [CustomExceptionFilter]
    public class AccountController : Controller
    {
        IUsersService userService;
        public AccountController(IUsersService userService)
        {
            this.userService = userService;
        }
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel registervm)
        {
          
                if (ModelState.IsValid && !GetIDPresentOrNot(registervm.Email))
                {
                    int userid = this.userService.InsertUser(registervm);
                    Session["CurrentUserID"] = userid;
                    Session["CurrentUserName"] = registervm.Name;
                    Session["CurrentUserEmail"] = registervm.Email;
                    Session["CurrentUserPassword"] = registervm.Password;
                    Session["CurrentUserIsAdmin"] = false;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x","Invalid Data");
                    return View();
                }

        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            string returnUrl = Convert.ToString(Request.QueryString["url"]);
            ViewBag.Message = returnUrl;
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginvm,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uservm = this.userService.GetUsersByEmailAndPassword(loginvm.Email, loginvm.Password);
                if (uservm != null)
                {
                    Session["CurrentUserID"] = uservm.UserID;
                    Session["CurrentUserName"] = uservm.Name;
                    Session["CurrentUserEmail"] = uservm.Email;
                    Session["CurrentUserPassword"] = uservm.Password;
                    Session["CurrentUserIsAdmin"] = uservm.IsAdmin;
                    if (uservm.IsAdmin)
                    {
                        if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                        {
                           return Redirect(Request.UrlReferrer.ToString());
                        }
                        else
                        {
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                        {
                            return Redirect(Request.UrlReferrer.ToString());
                        }
                        else
                        {
                           return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(loginvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(loginvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile()
        {
            int userid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.userService.GetUsersByUserID(userid);
            EditUserDetailsViewModel editvm = new EditUserDetailsViewModel() 
            { Name = uvm.Name, Email = uvm.Email, Mobile = uvm.Mobile, UserID = uvm.UserID };
            return View(editvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]       
        public ActionResult ChangeProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.userService.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int userid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uservm = this.userService.GetUsersByUserID(userid);
            EditUserPasswordViewModel editvm = new EditUserPasswordViewModel()
            { Email = uservm.Email, Password = "" , ConfirmPassword="", UserID = uservm.UserID };
            return View(editvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditUserPasswordViewModel editvm)
        {
            if (ModelState.IsValid)
            {
                editvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.userService.UpdateUserPassword(editvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(editvm);
            }
        }
        [NonAction]
        public bool GetIDPresentOrNot(string Email)
        {
            if (this.userService.GetUsersByEmail(Email) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}