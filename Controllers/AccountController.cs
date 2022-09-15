using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.CustomFilters;
using StackOverFlow.ServiceLayer;
using StackOverFlow.ViewModels;

namespace StackOverFlow.Controllers
{
    public class AccountController : Controller
    {
        IUserService us; //whenever a repo/service(dependency) is injected, we need to register it in the unity.mvc file
        public AccountController(UserService us)
        {
            this.us = us;
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = this.us.InsertUser(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }

        public ActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Login(LoginModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel loginModel = us.GetUserByEmailPass(lvm.Email, lvm.Password);
                if (loginModel != null)
                {
                    Session["CurrentUserID"] = loginModel.UserID;
                    Session["CurrentUserName"] = loginModel.Name;
                    Session["CurrentUserEmail"] = loginModel.Email;
                    Session["CurrentUserPassword"] = loginModel.PasswordHash;
                    Session["CurrentUserIsAdmin"] = loginModel.IsAdmin;
                    //return RedirectToAction("About", "Home");

                    if (loginModel.IsAdmin)
                        return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "Index" });
                    else return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("x", "Invalid Email/Password");
                    return View(loginModel);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(lvm);
            }


        }


        [UserAuthorizationFilter]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel user = this.us.GetUserByUserID(uid);
            EditUserModel editUser = new EditUserModel() { Name = user.Name, Email = user.Email, Mobile = user.Mobile, UserID= user.UserID };
            //us.UpdateUserDetails(editUser);
            return View(editUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangeProfile(EditUserModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel user = this.us.GetUserByUserID(uid);
            EditUserPassword editUserPass = new EditUserPassword() {  Email = user.Email,  UserID = user.UserID, Password="", ConfirmPassword="" };
            //us.UpdateUserDetails(editUser);
            return View(editUserPass);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangePassword(EditUserPassword eupass)
        {
            if (ModelState.IsValid)
            {
                eupass.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.us.UpdateUserPassword(eupass);
                Session["CurrentUserID"] = eupass.UserID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupass);
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}
